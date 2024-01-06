using System.Text;

using Foundation.Exceptions.Datasources;
using Foundation.Managers;

using Microsoft.EntityFrameworkCore;

using TWS_Security.Entities;
using TWS_Security.Repositories;
using TWS_Security.Sets;
using Foundation.Enumerators.Records;

using Xunit;

using EntityReferenceResults = Foundation.Records.Datasources.OperationResults<TWS_Security.Entities.AccountEntity, TWS_Security.Entities.AccountEntity>;

namespace TWS_Security.Quality.Repositories;
public class Q_AccountsRepository {
    private readonly TWSSecuritySource Source;
    private readonly AccountsRepository Repo;
    private readonly AccountEntity[] Mocks = [];

    public Q_AccountsRepository() {
        Source = new TWSSecuritySource();
        Repo = new();

        for (int p = 0; p < 5; p++) {
            byte[] rp = Encoding.Unicode.GetBytes(RandomManager.String(8));
            string ru = RandomManager.String(7);

            AccountEntity re = new(ru, rp);
            Mocks = [.. Mocks, re];
        }
    }

    [Fact]
    public async void Create() {
        AccountEntity FirstFact = await Repo.Create(Mocks[0]);
        EntityReferenceResults SecondFact = await Repo.Create(Mocks[1], 3);
        EntityReferenceResults ThirdFact = await Repo.Create([Mocks[2], Mocks[3], Mocks[4], Mocks[2], Mocks[3]]);

        #region First Fact Asserts (Creating a single entity)
        Assert.Multiple([
            () => Assert.True(FirstFact.Pointer > 0),
            () => Assert.ThrowsAsync<XUniqueViolation>(() => Repo.Create(Mocks[0])),
            () => {
                AccountEntity Entity = FirstFact;
                Account Set = Source.Accounts
                    .Where(i => i.Id == Entity.Pointer)
                    .FirstOrDefault()
                    ?? throw new Exception($"Item wasn't saved correctly {nameof(FirstFact)}");

                Assert.Equal(Entity.Pointer, Set.Id);
                Assert.Equal(Entity.User, Set.User);
                Assert.True(Entity.Password.SequenceEqual(Set.Password));

                Assert.True(Entity.EvaluateSet(Set));

                Source.Remove(Set);
                Source.SaveChanges();
            },
        ]);
        #endregion  

        #region Second Fact Asserts (Creating copies of a Entity)
        Assert.Multiple([
            () => Assert.Single(SecondFact.Successes),
            () => Assert.Equal(2, SecondFact.Failures.Count),
            () => Assert.True(SecondFact.Successes[0].Pointer > 0),
            () => {
                Assert.All(SecondFact.Failures, 
                    (I) => {
                    Assert.Equal(OperationFailureCriterias.Entity, I.Criteria);
                });
            },
            () => {
                AccountEntity Entity = SecondFact.Successes[0];
                Account Set = Source.Accounts
                    .Where(i => i.Id == Entity.Pointer)
                    .AsNoTracking()
                    .FirstOrDefault()
                    ?? throw new Exception($"Item wasn't saved correctly {nameof(SecondFact)}");

                Assert.Equal(Entity.Pointer, Set.Id);
                Assert.Equal(Entity.User, Set.User);
                Assert.True(Entity.Password.SequenceEqual(Set.Password));

                Assert.True(Entity.EvaluateSet(Set));

                Source.Remove(Set);
                Source.SaveChanges();
            },
        ]);
        #endregion  

        #region Third Fact Asserts (Creating a collection of Entities) 
        Assert.Multiple([
            () => Assert.Equal(3, ThirdFact.Successes.Count),
            () => Assert.Equal(2, ThirdFact.Failures.Count),
            () => {
                Assert.All(ThirdFact.Failures, 
                    (I) => {
                        Assert.Equal(OperationFailureCriterias.Entity, I.Criteria);
                });
            },
            () => {
                Assert.All(ThirdFact.Successes,
                    (I) => {
                        Assert.True(I.Pointer > 0);

                        Account Set = Source.Accounts
                            .Where(T => T.Id == I.Pointer)
                            .AsNoTracking()
                            .FirstOrDefault()
                            ?? throw new Exception($"Item wasn't saved correctly {nameof(ThirdFact)}({ThirdFact.Successes.IndexOf(I)})");

                        Assert.Equal(I.Pointer, Set.Id);
                        Assert.Equal(I.User, Set.User);
                        Assert.True(I.Password.SequenceEqual(Set.Password));

                        Assert.True(I.EvaluateSet(Set));

                        Source.Remove(Set);
                        Source.SaveChanges();
                    });
            }
        ]);
        #endregion
    } 

    [Fact]
    public async void Read() {
        #region Pre-Tests 
        Account Set = Mocks[0].BuildSet();
        await Source.AddAsync(Set);
        await Source.SaveChangesAsync();
        #endregion 

        try {
            List<AccountEntity> FirstFact = await Repo.Read();

            #region First Fact Asserts (Reading all the Accounts in the repository) 
            Assert.Multiple([
                () => Assert.NotEmpty(FirstFact),
                () => Assert.Contains(Mocks[0], FirstFact),
            ]);
            #endregion

            #region After-Tests 
            Source.Remove(Set);
            Source.SaveChanges();
            #endregion
        } catch {
            Source.Remove(Set);
            Source.SaveChanges();
            throw;
        }
    }
}
