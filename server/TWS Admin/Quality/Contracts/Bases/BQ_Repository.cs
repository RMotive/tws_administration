using Foundation;
using Foundation.Contracts.Datasources.Bases;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Enumerators.Exceptions;
using Foundation.Enumerators.Records;
using Foundation.Exceptions.Datasources;
using Foundation.Managers;
using Foundation.Records.Datasources;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TWS_Security.Quality.Contracts.Interfaces;

using Xunit;

namespace TWS_Security.Quality.Contracts.Bases;
/// <summary>
///     Base for [Q_Repository].
///     
///     Defines useful behaviors to a [Q_Repository] implementation.
///     
///     [Q_Repository] concept: a quality class to evaluate Repository concept implementations.
/// </summary>
public abstract class BQ_Repository<TLive, TRepository, TEntity, TSet>
    : IQ_Repository
    where TLive : DbContext
    where TRepository : IRepository<TEntity, TSet>
    where TEntity : IEntity<TSet>
    where TSet : BSet<TSet, TEntity> {

    #region Properties

    /// <summary>
    ///     Test managed data source.
    /// </summary>
    protected readonly TLive Live;
    /// <summary>
    ///     Test managed repository implementation.
    /// </summary>
    protected readonly TRepository Repository;
    /// <summary>
    ///     Stores the failure secondMock
    /// </summary>
    protected readonly (TSet Sets, TEntity Entity) XMock;
    /// <summary>
    ///     Stores collection of mocks to use (live). 
    /// </summary>
    protected (TSet[] Sets, TEntity[] Entities) UMocks { get; private set; }
    /// <summary>
    ///     Stores collection of mocks to use (not live, just objects). 
    /// </summary>
    protected (TSet[] Sets, TEntity[] Entities) Mocks { get; private set; }

    private (TSet[] Sets, TEntity[] Entities) MOCKS;

    #endregion

    #region Constructors

    public BQ_Repository(int QtyMocks, TLive Live, TRepository Repository, Func<string, TSet, TSet> UpdateActiuon) {
        this.Live = Live;
        this.Repository = Repository;
        this.UpdateAction = UpdateActiuon;
        XMock = XMockFactory();
        Mocks = GenerateMocks(QtyMocks);
        MOCKS = GenerateMocks(MOCKS_QTY);
    }

    #endregion

    #region Private Methods

    private (TSet[] Sets, TEntity[] Entities) GenerateMocks(int Quantity) {
        TEntity[] entities = [];
        TSet[] sets = [];

        for (int i = 0; i < Quantity; i++) {
            string token = $"Q_{RandomManager.String(8)}";
            (TSet set, TEntity entity) mock = MockFactory(i, token);

            entities = [.. entities, mock.entity];
            sets = [.. sets, mock.set];
        }

        return (sets, entities);
    }

    #endregion

    #region Protected Abstract Methods

    protected abstract TSet UMockFactory(int Pointer, string Token);

    protected abstract (TSet Set, TEntity Entity) XMockFactory();

    protected abstract (TSet Set, TEntity Entity) MockFactory(int Pointer, string Token);

    #endregion

    #region Protected Methods

    protected void Restore() {
        if (UMocks.Sets.IsNullOrEmpty()) return;

        try {
            Live.ChangeTracker.Clear();
            Live.RemoveRange(UMocks.Sets);
            Live.SaveChanges();
        } catch { }

        UMocks = ([], []);
    }
    protected void Restore(TSet Set) {
        Live.Remove(Set);
        Live.SaveChanges();
    }
    protected void Restore(TSet[] Sets) {
        Live.RemoveRange(Sets);
        Live.SaveChanges();
    }

    protected TSet Search(TEntity Entity, string Identifier) {
        TSet set = Live
            .Set<TSet>()
            .Where(i => i.Id == Entity.Pointer)
            .FirstOrDefault()
            ?? throw new XRecordUnfound<TRepository>($"nameof(Search) over {Identifier} ", Entity.Pointer, RecordSearchMode.ByPointer);
        return set;
    }

    protected void GenerateUMocks(int Quantity) {
        if (!UMocks.Sets.IsNullOrEmpty()) {
            try {
                Restore();
            } catch { };
        }

        TEntity[] entities = [];
        TSet[] sets = [];
        for (int i = 0; i < Quantity; i++) {
            string token = $"Q_{RandomManager.String(8)}";
            TSet set = UMockFactory(i, token);

            Live.Add(set);
            Live.SaveChanges();

            TEntity entity = set.GenerateEntity();

            entities = [.. entities, entity];
            sets = [.. sets, set];
        }

        UMocks = (sets, entities);
    }

    protected void PushLiveMock(TSet set, TEntity entity) {
        UMocks = ([.. UMocks.Sets, set], [.. UMocks.Entities, entity]);
    }
    #endregion

    #region Public Methods

    readonly Func<string, TSet, TSet> UpdateAction;
    const int MOCKS_QTY = 5;

    /// <summary>
    ///     Mock usage:
    ///         MOCKS: 0, 1, 2, 3, 4
    /// </summary>
    [Fact]
    public async void Create() {
        TEntity[] mocks = MOCKS.Entities;

        TEntity firstFact = await Repository.Create(mocks[0]);
        OperationResults<TEntity, TEntity> secondFact = await Repository.Create(mocks[1], 3);
        OperationResults<TEntity, TEntity> thirdFact = await Repository.Create([mocks[2], mocks[3], mocks[4], mocks[2], mocks[3]]);

        #region First Fact Asserts (Creating a single entity)

        Assert.Multiple([
            () => Assert.True(firstFact.Pointer > 0),
            () => Assert.ThrowsAsync<XUniqueViolation<TSet>>(() => Repository.Create(mocks[0])),
            () => {
                TEntity entity = firstFact;

                TSet set = Search(entity, nameof(firstFact));
                Assert.True(entity.EqualsSet(set));

                Restore(set);
            },
        ]);

        #endregion  

        #region Second Fact Asserts (Creating copies of a Entity)

        Assert.Multiple([
            () => Assert.Single(secondFact.Successes),
            () => Assert.Equal(2, secondFact.Failures.Count),
            () => Assert.True(secondFact.Successes[0].Pointer > 0),
            () => {
                Assert.All(secondFact.Failures,
                    (I) => {
                        Assert.Equal(OperationFailureCriterias.Entity, I.Criteria);
                    });
            },
            () => {
                TEntity entity = secondFact.Successes[0];
                TSet set = Search(entity, nameof(secondFact));

                Assert.True(entity.EqualsSet(set));
                Restore(set);
            },
        ]);

        #endregion  

        #region Third Fact Asserts (Creating a collection of Entities) 
        Assert.Multiple([
            () => Assert.Equal(3, thirdFact.Successes.Count),
            () => Assert.Equal(2, thirdFact.Failures.Count),
            () => {
                Assert.All(thirdFact.Failures,
                    (I) => {
                        Assert.Equal(OperationFailureCriterias.Entity, I.Criteria);
                    });
            },
            () => {
                Assert.All(thirdFact.Successes,
                    (I, Pointer) => {
                        Assert.True(I.Pointer > 0);

                        TSet Set = Search(I, $"{Pointer}");
                        Assert.Equal(I.Pointer, Set.Id);
                        Assert.True(I.EqualsSet(Set));
                        Restore(Set);
                    });
            }
        ]);
        #endregion
    }
    /// <summary>
    ///     Mock usage:
    ///         UMocks: [1]
    /// </summary>
    [Fact]
    public async void Read() {
        #region Pre-Tests 

        GenerateUMocks(1);
        (TSet Set, TEntity Entity) mock;
        {
            TSet set = UMocks.Sets[0];
            TEntity entity = UMocks.Entities[0];

            mock = (set, entity);
        }

        #endregion 

        try {
            CriticalOperationResults<TEntity, TSet> firstFact = await Repository.Read();
            CriticalOperationResults<TEntity, TSet> secondFact = await Repository.Read(Behavior: ReadingBehavior.First);
            CriticalOperationResults<TEntity, TSet> thirdFact = await Repository.Read(Behavior: ReadingBehavior.Last);
            TEntity fourthFact = await Repository.Read(mock.Entity.Pointer);
            CriticalOperationResults<TEntity, TSet> fifthFact = await Repository.Read([mock.Entity.Pointer, 1000000000]);

            #region First Fact Asserts (Reading all no filter) 

            Assert.Multiple([
                () => Assert.NotEmpty(firstFact.Successes),
                () => Assert.Contains(mock.Entity, firstFact.Successes),
                () => Assert.Empty(firstFact.Failures),
            ]);

            #endregion

            #region Second Fact Asserts (Reading first no filter) 

            Assert.Multiple([
                () => Assert.NotEmpty(secondFact.Successes),
                () => Assert.Empty(secondFact.Failures),
                () => Assert.Equal(1, secondFact.Succeeded),
                () => Assert.Equal(0, secondFact.Failed),
                () => Assert.Equal(1, secondFact.Results),
                () => {
                    try {
                        TSet set = Live.Set<TSet>().ToArray()[0];
                        TEntity entity = set.GenerateEntity();

                        Assert.Equal(entity, secondFact.Successes[0]);
                    } catch {
                        // --> This means that the first record in the live database set
                        // is an invalid one and should be debugged.
                        // DEVELOPER NOTE.
                        Assert.NotEmpty(secondFact.Failures);
                        Assert.Empty(secondFact.Successes);
                        Assert.Equal(1, secondFact.Failed);
                        Assert.Equal(0, secondFact.Succeeded);
                        Assert.Equal(1, secondFact.Results);
                    }
                }
            ]);

            #endregion

            #region Third Fact Asserts (Reading last no filter)

            Assert.Multiple([
                () => Assert.Empty(thirdFact.Failures),
                () => Assert.NotEmpty(thirdFact.Successes),
                () => Assert.Equal(1, thirdFact.Results),
                () => Assert.Equal(1, thirdFact.Succeeded),
                () => Assert.Equal(0, thirdFact.Failed),
                () => {
                    try {
                        TSet set = Live.Set<TSet>().ToArray()[^1];
                        TEntity entity = set.GenerateEntity();

                        Assert.Equal(entity, thirdFact.Successes[0]);
                    } catch {
                        Assert.Empty(thirdFact.Successes);
                        Assert.NotEmpty(thirdFact.Failures);
                        Assert.Equal(1, thirdFact.Results);
                        Assert.Equal(1, thirdFact.Failed);
                        Assert.Equal(0, thirdFact.Succeeded);
                    }
                }
            ]);

            #endregion

            #region Fourth Fact Asserts (Reading by pointer)

            Assert.Multiple([
                () => Assert.True(fourthFact.Pointer > 0),
                () => Assert.Equal(mock.Entity, fourthFact),
                () => Assert.ThrowsAsync<XRecordUnfound<TRepository>>(async () => await Repository.Read(1000000000)),
            ]);

            #endregion

            #region Fifth Fact Asserts (Reading pointer collection)

            Assert.Multiple([
                () => Assert.NotEmpty(fifthFact.Failures),
                () => Assert.NotEmpty(fifthFact.Successes),
                () => Assert.Equal(2, fifthFact.Results),
                () => Assert.Equal(1, fifthFact.Failed),
                () => Assert.Equal(1, fifthFact.Succeeded),
                () => {
                    TEntity Successed = fifthFact.Successes[0];
                    Assert.True(Successed.Pointer > 0);
                    Assert.Equal(mock.Entity, Successed);
                },
                () => {
                    OperationFailure<TSet> Failed = fifthFact.Failures[0];
                    Assert.Equal(typeof(TSet), Failed.Type);
                    Assert.Equal(1000000000, Failed.Reference.Id);
                    Assert.Equal(OperationFailureCriterias.Pointer, Failed.Criteria);
                    Assert.IsType<XRecordUnfound<TRepository>>(Failed.Failure);
                }
            ]);

            #endregion

        } finally {
            Restore(mock.Set);
        }
    }
    /// <summary>
    ///     Mock usage:
    ///         MOCKS: 0
    ///         UMocks: [1]
    /// </summary>
    [Fact]
    public async void Update() {
        try {
            #region Pre-Tests

            GenerateUMocks(1);
            (TSet Set, TEntity Entity) firstMock;
            {
                TSet set = UMocks.Sets[0].Clone();
                set = UpdateAction(RandomManager.String(8), set);
                TEntity entity = set.GenerateEntity();
                firstMock = (set, entity);
            }

            #endregion

            TEntity firstFact = await Repository.Update(firstMock.Entity);
            TEntity secondFact = await Repository.Update(MOCKS.Entities[0], true);

            #region First Fact (Asserts) [Updating an existing record no fallback]

            Assert.Multiple([
                () => Assert.NotEqual(firstMock.Set, UMocks.Sets[0]),
                () => UMocks.Sets[0] = firstMock.Set,
                () => Assert.True(firstFact.Pointer > 0),
                () => Assert.True(firstFact.EqualsSet(firstMock.Set)),
                () => Assert.ThrowsAsync<XRecordUnfound<TRepository>>(async () => await Repository.Update(XMock.Entity)),
            ]);

            #endregion

            #region Second Fact (Asserts) [Updating an unexisting record with fallback] 


            Assert.Multiple([
                () => {
                    TEntity entity = secondFact;
                    TSet set = secondFact.GenerateSet();

                    PushLiveMock(set, entity);
                },
                () => Assert.Equal(secondFact, MOCKS.Entities[0]),
            ]);

            #endregion

            /*
                Actually is unnecessary to test the case when updating an unexsting record with no fallback
                cause that simple assertion is being checked up in the fifth assert line of the First Fact Asserts region
            */
        } finally {
            Restore();
        }
    }
    /// <summary>
    ///     Mock usage:
    ///         UMocks: [1]
    /// </summary>
    [Fact]
    public async void Delete() {
        #region Pre-Tests

        GenerateUMocks(1);
        (TSet set, TEntity entity) = (UMocks.Sets[0], UMocks.Entities[0]);

        #endregion
        try {
            await Repository.Delete(entity);

            #region First Fact (Asserts) [Deleting an existing record]

            Assert.Throws<XRecordUnfound<TRepository>>(() => Search(entity, "Q_Delete"));

            #endregion
        } finally {
            Restore();
        }
    }

    #endregion
}
