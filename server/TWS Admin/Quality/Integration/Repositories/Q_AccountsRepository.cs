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

using CriticalOperationResults = Foundation.Records.Datasources.CriticalOperationResults<TWS_Security.Entities.AccountEntity, TWS_Security.Sets.Account>;
using EntityReferenceResults = Foundation.Records.Datasources.OperationResults<TWS_Security.Entities.AccountEntity, TWS_Security.Entities.AccountEntity>;

namespace TWS_Security.Quality.Integration.Repositories;
public class Q_AccountsRepository
    : BQ_Repository<TWSSecuritySource, AccountsRepository, AccountEntity, Account> {
    public Q_AccountsRepository() : base(new(), new()) { }

    protected override (AccountEntity[] Mocks, (Account[] Sets, AccountEntity[] Entities) XMocks) InitMocks() {
        AccountEntity[] mocks = [];
        for (int p = 0; p < 5; p++) {
            byte[] rp = Encoding.Unicode.GetBytes(RandomManager.String(8));
            string ru = RandomManager.String(7);

            AccountEntity re = new(ru, rp, false);
            mocks = [.. mocks, re];
        }

        AccountEntity[] entities = [];
        Account[] sets = Array.Empty<Account>();
        for (int P = 0; P < 2; P++) {
            string RandomtString = RandomManager.String(12);
            Account set = new() {
                Id = int.MaxValue - P,
                User = RandomtString,
                Password = Encoding.Unicode.GetBytes(RandomtString),
            };
            AccountEntity entity = set.GenerateEntity();

            sets = [.. sets, set];
            entities = [.. entities, entity];
        }

        return (mocks, (sets, entities));
    }

    protected override async Task<(Account[] Sets, AccountEntity[] Entities)> InitLiveMocks() {
        AccountEntity[] entities = [];
        Account[] sets = [];

        for (int P = 0; P < 2; P++) {
            string random = RandomManager.String(7);
            Account set = new() {
                User = random,
                Password = Encoding.ASCII.GetBytes(random),
            };

            await Live.AddAsync(set);
            await Live.SaveChangesAsync();
            Live.ChangeTracker.Clear();
            AccountEntity entity = set.GenerateEntity();

            sets = [.. sets, set];
            entities = [.. entities, entity];
        }
        return (sets, entities);
    }

    public override async void Create() {
        AccountEntity firstFact = await Repository.Create(Mocks[0]);
        EntityReferenceResults secondFact = await Repository.Create(Mocks[1], 3);
        EntityReferenceResults thirdFact = await Repository.Create([Mocks[2], Mocks[3], Mocks[4], Mocks[2], Mocks[3]]);

        #region First Fact Asserts (Creating a single entity)

        Assert.Multiple([
            () => Assert.True(firstFact.Pointer > 0),
            () => Assert.ThrowsAsync<XUniqueViolation<Account>>(() => Repository.Create(Mocks[0])),
            () => {
                AccountEntity Entity = firstFact;

                Account Set = Search(Entity, nameof(firstFact));
                Assert.Equal(Entity.Pointer, Set.Id);
                Assert.Equal(Entity.User, Set.User);
                Assert.True(Entity.EqualsSet(Set));
                Assert.True(Entity.Password.SequenceEqual(Set.Password));

                Restore(Set);
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
                AccountEntity Entity = secondFact.Successes[0];
                Account Set = Search(Entity, nameof(secondFact));

                Assert.Equal(Entity.Pointer, Set.Id);
                Assert.Equal(Entity.User, Set.User);
                Assert.True(Entity.Password.SequenceEqual(Set.Password));

                Assert.True(Entity.EqualsSet(Set));

                Restore(Set);
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

                        Account Set = Search(I, $"{Pointer}");

                        Assert.Equal(I.Pointer, Set.Id);
                        Assert.Equal(I.User, Set.User);
                        Assert.True(I.Password.SequenceEqual(Set.Password));

                        Assert.True(I.EqualsSet(Set));

                        Restore(Set);
                    });
            }
        ]);
        #endregion
    }

    public override async void Read() {

        #region Pre-Tests 

        (Account set, AccountEntity entity) mock;
        {
            Account set = Mocks[0].GenerateSet();
            await Live.AddAsync(set);
            await Live.SaveChangesAsync();
            AccountEntity entity = set.GenerateEntity();

            mock = (set, entity);
        }

        #endregion 

        try {
            CriticalOperationResults firstFact = await Repository.Read();
            CriticalOperationResults secondFact = await Repository.Read(Behavior: ReadingBehavior.First);
            CriticalOperationResults thirdFact = await Repository.Read(Behavior: ReadingBehavior.Last);
            AccountEntity fourthFact = await Repository.Read(mock.entity.Pointer);
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
                        Account firstRecord = Live.Accounts.ToArray()[0];
                        AccountEntity firstEntity = firstRecord.GenerateEntity();

                        Assert.Equal(firstEntity, secondFact.Successes[0]);
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
                        Account Record = Live.Accounts.ToArray()[^1];
                        AccountEntity RecordEntity = Record.GenerateEntity();

                        Assert.Equal(RecordEntity, thirdFact.Successes[0]);
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
                () => Assert.ThrowsAsync<XRecordUnfound<AccountsRepository>>(async () => await Repository.Read(1000000000)),
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
                    AccountEntity Successed = fifthFact.Successes[0];
                    Assert.True(Successed.Pointer > 0);
                    Assert.Equal(mock.entity, Successed);
                },
                () => {
                    OperationFailure<Account> Failed = fifthFact.Failures[0];
                    Assert.Equal(typeof(Account), Failed.Type);
                    Assert.Equal(1000000000, Failed.Reference.Id);
                    Assert.Equal(OperationFailureCriterias.Pointer, Failed.Criteria);
                    Assert.IsType<XRecordUnfound<AccountsRepository>>(Failed.Failure);
                }
            ]);
            #endregion

        } finally {
            Restore(mock.set);
        }
    }

    public override async void Update() {
        try {
            #region Pre-Tests

            await GenerateLiveMocks();
            (Account Set, AccountEntity Entity) firstFactMocks;
            {
                Account FirstFactSet = LiveMocks.Sets[0].Clone();
                FirstFactSet.User = RandomManager.String(9);
                AccountEntity FirstFactEty = FirstFactSet.GenerateEntity();
                firstFactMocks = (FirstFactSet, FirstFactEty);
            }

            #endregion

            AccountEntity firstFact = await Repository.Update(firstFactMocks.Entity);
            AccountEntity secondFact = await Repository.Update(XMocks.Entities[0], true);

            #region First Fact (Asserts) [Updating an existing record no fallback]

            Assert.Multiple([
                () => Assert.NotEqual(firstFactMocks.Set, LiveMocks.Sets[0]),
                () => LiveMocks.Sets[0] = firstFactMocks.Set,
                () => Assert.True(firstFact.Pointer > 0),
                () => Assert.True(firstFact.EqualsSet(firstFactMocks.Set)),
                () => Assert.ThrowsAsync<XRecordUnfound<AccountsRepository>>(async () => await Repository.Update(XMocks.Entities[0])),
            ]);

            #endregion

            #region Second Fact (Asserts) [Updating an unexisting record with fallback] 


            Assert.Multiple([
                () => {
                    AccountEntity entity = secondFact;
                    Account set = secondFact.GenerateSet();

                    UpdateLiveMocks([.. LiveMocks.Sets, set], [.. LiveMocks.Entities, entity]);
                },
                () => Assert.True(secondFact.Pointer > 0),
                () => Assert.Equal(secondFact.User, XMocks.Entities[0].User),
                () => Assert.True(secondFact.Password.SequenceEqual(XMocks.Entities[0].Password)),
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

    public override async void Delete() {
        #region Pre-Tests

        await GenerateLiveMocks();

        (Account set, AccountEntity entity) = (LiveMocks.Sets[0], LiveMocks.Entities[0]);

        #endregion
        try {
            await Repository.Delete(entity);

            #region First Fact (Asserts) [Deleting an existing record]

            Assert.Throws<XRecordUnfound<AccountsRepository>>(() => Search(entity, ""));

            #endregion
        } finally {
            UpdateLiveMocks(LiveMocks.Sets[1..], LiveMocks.Entities[1..]);
            Restore();
        }
    }
}
