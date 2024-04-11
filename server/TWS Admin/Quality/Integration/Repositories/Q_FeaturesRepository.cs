using System.Text;

using Foundation;
using Foundation.Enumerators.Records;
using Foundation.Exceptions.Datasources;
using Foundation.Managers;
using Foundation.Records.Datasources;

using TWS_Security.Entities;
using TWS_Security.Quality.Contracts.Bases;
using TWS_Security.Repositories;
using TWS_Security.Sets;

using Xunit;

using CriticalOperationResults = Foundation.Records.Datasources.CriticalOperationResults<TWS_Security.Entities.FeatureEntity, TWS_Security.Sets.Feature>;
using EntityReferenceResults = Foundation.Records.Datasources.OperationResults<TWS_Security.Entities.FeatureEntity, TWS_Security.Entities.FeatureEntity>;

namespace TWS_Security.Quality.Integration.Repositories;
public class Q_FeaturesRepository
    : BQ_Repository<TWSSecuritySource, FeaturesRepository, FeatureEntity, Feature> {

    const int mocksQty = 5;
    const int liveMocksQty = 1;
    const int xMocksQty = 1;

    public Q_FeaturesRepository() : base(new(), new()) { }

    protected override (FeatureEntity[] Mocks, (Feature[] Sets, FeatureEntity[] Entities) XMocks) InitMocks() {
        FeatureEntity[] mocks = [];
        for (int i = 0; i < mocksQty; i++) {
            string name = RandomManager.String(7);
            string description = $"Quality description for {name}"; 

            FeatureEntity entity = new(name, description);
            mocks = [.. mocks, entity];
        }

        Feature[] sets = [];
        FeatureEntity[] entities = [];
        for (int i = 0; i < xMocksQty; i++) {
            string name = RandomManager.String(12);
            Feature set = new() {
                Id = int.MaxValue - i,
                Name = name,
                Description = $"Description for {name}",
            };
            FeatureEntity entity = set.GenerateEntity();

            sets = [.. sets, set];
            entities = [.. entities, entity];
        }

        return (mocks, (sets, entities));
    }
    protected override async Task<(Feature[] Sets, FeatureEntity[] Entities)> InitLiveMocks() {
        Feature[] sets = [];
        FeatureEntity[] entities = [];

        for (int i = 0; i < liveMocksQty; i++) {
            string name = RandomManager.String(7);
            Feature set = new() {
                Name = name,
                Description = $"Description for {name}",
            };

            await Live.AddAsync(set);
            await Live.SaveChangesAsync();
            Live.ChangeTracker.Clear();
            FeatureEntity entity = set.GenerateEntity();

            sets = [.. sets, set];
            entities = [.. entities, entity];
        }
        return (sets, entities);
    }

    /// <summary>
    ///     Mock usage:
    ///         Mocks: 0, 1, 2, 3, 4
    /// </summary>
    public override async void Create() {
        FeatureEntity firstFact = await Repository.Create(Mocks[0]);
        EntityReferenceResults secondFact = await Repository.Create(Mocks[1], 3);
        EntityReferenceResults thirdFact = await Repository.Create([Mocks[2], Mocks[3], Mocks[4], Mocks[2], Mocks[3]]);

        #region First Fact Asserts (Creating a single entity)

        Assert.Multiple([
            () => Assert.True(firstFact.Pointer > 0),
            () => Assert.ThrowsAsync<XUniqueViolation<Feature>>(() => Repository.Create(Mocks[0])),
            () => {
                FeatureEntity entity = firstFact;

                Feature set = Search(entity, nameof(firstFact));
                Assert.Equal(entity.Pointer, set.Id);
                Assert.Equal(entity.Name, set.Name);
                Assert.Equal(entity.Description, set.Description);
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
                FeatureEntity entity = secondFact.Successes[0];
                Feature set = Search(entity, nameof(secondFact));

                Assert.Equal(entity.Pointer, set.Id);
                Assert.Equal(entity.Name, set.Name);
                Assert.Equal(entity.Description, set.Description);

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
                    (entity, Pointer) => {
                        Assert.True(entity.Pointer > 0);

                        Feature set = Search(entity, $"{Pointer}");

                        Assert.Equal(entity.Pointer, set.Id);
                        Assert.Equal(entity.Name, set.Name);
                        Assert.Equal(entity.Description, set.Description);

                        Assert.True(entity.EqualsSet(set));

                        Restore(set);
                    });
            }
        ]);

        #endregion
    }

    /// <summary>
    ///     Mock usage:
    ///         Mocks: 0
    /// </summary>
    public override async void Read() {
        #region Pre-Tests 

        (Feature set, FeatureEntity entity) mock;
        {
            Feature set = Mocks[0].GenerateSet();
            await Live.AddAsync(set);
            await Live.SaveChangesAsync();
            FeatureEntity entity = set.GenerateEntity();

            mock = (set, entity);
        }

        #endregion

        try {
            CriticalOperationResults firstFact = await Repository.Read();
            CriticalOperationResults secondFact = await Repository.Read(Behavior: ReadingBehavior.First);
            CriticalOperationResults thirdFact = await Repository.Read(Behavior: ReadingBehavior.Last);
            FeatureEntity fourthFact = await Repository.Read(mock.entity.Pointer);
            CriticalOperationResults fifthFact = await Repository.Read([mock.entity.Pointer, 1000000000]);

            #region First Fact Asserts (Reading all no filter) 

            Assert.Multiple([
                () => Assert.NotEmpty(firstFact.Successes),
                () => Assert.Contains(mock.entity, firstFact.Successes),
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
                        Feature set = Live.Features.ToArray()[0];
                        FeatureEntity entity = set.GenerateEntity();

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
                        Feature set = Live.Features.ToArray()[^1];
                        FeatureEntity entity = set.GenerateEntity();

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
                () => Assert.Equal(mock.entity, fourthFact),
                () => Assert.ThrowsAsync<XRecordUnfound<FeaturesRepository>>(async () => await Repository.Read(1000000000)),
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
                    FeatureEntity entity = fifthFact.Successes[0];
                    Assert.True(entity.Pointer > 0);
                    Assert.Equal(mock.entity, entity);
                },
                () => {
                    OperationFailure<Feature> failure = fifthFact.Failures[0];
                    Assert.Equal(typeof(Account), failure.Type);
                    Assert.Equal(1000000000, failure.Reference.Id);
                    Assert.Equal(OperationFailureCriterias.Pointer, failure.Criteria);
                    Assert.IsType<XRecordUnfound<AccountsRepository>>(failure.Failure);
                }
            ]);

            #endregion

        } finally {
            Restore(mock.set);
        }
    }

    /// <summary>
    ///     Mock usage:
    ///         LiveMocks: 0,
    ///         XMocks: 0,
    /// </summary>
    public override async void Update() {
        try {
            #region Pre-Tests

            await GenerateLiveMocks();
            (Feature Set, FeatureEntity Entity) firstFactMock;
            {
                Feature set = LiveMocks.Sets[0].Clone();
                set.Name = RandomManager.String(9);
                FeatureEntity entity = set.GenerateEntity();
                firstFactMock = (set, entity);
            }

            #endregion

            FeatureEntity firstFact = await Repository.Update(firstFactMock.Entity);
            FeatureEntity secondFact = await Repository.Update(XMocks.Entities[0], true);

            #region First Fact (Asserts) [Updating an existing record no fallback]

            Assert.Multiple([
                () => Assert.NotEqual(firstFactMock.Set, LiveMocks.Sets[0]),
                () => LiveMocks.Sets[0] = firstFactMock.Set,
                () => Assert.True(firstFact.Pointer > 0),
                () => Assert.True(firstFact.EqualsSet(firstFactMock.Set)),
                () => Assert.ThrowsAsync<XRecordUnfound<AccountsRepository>>(async () => await Repository.Update(XMocks.Entities[0])),
            ]);

            #endregion

            #region Second Fact (Asserts) [Updating an unexisting record with fallback] 

            Assert.Multiple([
                () => {
                    FeatureEntity entity = secondFact;
                    Feature set = secondFact.GenerateSet();

                    UpdateLiveMocks([.. LiveMocks.Sets, set], [.. LiveMocks.Entities, entity]);
                },
                () => Assert.True(secondFact.Pointer > 0),
                () => Assert.Equal(secondFact.Name, XMocks.Entities[0].Name),
                () => Assert.Equal(secondFact.Description, XMocks.Entities[0].Description),
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

    public override void Delete() {
        throw new NotImplementedException();
    }
}
