using Foundation.Exceptions.Datasources;
using Foundation.Managers;
using Foundation.Records.Datasources;

using Microsoft.EntityFrameworkCore;

using System.Text;

using TWS_Security.Entities;
using TWS_Security.Repositories;
using TWS_Security.Sets;

using Xunit;

namespace TWS_Security.Quality.Repositories;

public class Q_AccountsRepository {
    private readonly TWSSecuritySource _source;
    private readonly AccountsRepository _repo;
    private readonly AccountEntity[] _mocks = [];

    public Q_AccountsRepository() {
        _source = new TWSSecuritySource();
        _repo = new();

        for(int p = 0; p < 5; p++) {
            byte[] rp = Encoding.Unicode.GetBytes(RandomManager.String(8));
            string ru = RandomManager.String(7);

            AccountEntity re = new(ru, rp);
            _mocks = [.._mocks, re];
        }
    }

    [Fact]
    public async void Create() {
        AccountEntity FirstFact = await _repo.Create(_mocks[0]);
        CreationResults<AccountEntity> SecondFact = await _repo.Create(_mocks[1], 3);
        CreationResults<AccountEntity> ThirdFact = await _repo.Create([_mocks[2], _mocks[3], _mocks[4], _mocks[2], _mocks[3]]);

        #region First Fact Asserts
        Assert.Multiple([
            () => Assert.True(FirstFact.Pointer > 0),
            () => Assert.ThrowsAsync<XUniqueViolation>(() => _repo.Create(_mocks[0])),
            () => {
                AccountEntity Entity = FirstFact;
                Account Set = _source.Accounts
                    .Where(i => i.Id == Entity.Pointer)
                    .FirstOrDefault() 
                    ?? throw new Exception($"Item wasn't saved correctly {nameof(FirstFact)}");

                Assert.Equal(Entity.Pointer, Set.Id);
                Assert.Equal(Entity.User, Set.User);
                Assert.True(Entity.Password.SequenceEqual(Set.Password));

                Assert.True(Entity.EvaluateSet(Set));

                _source.Remove(Set);
                _source.SaveChanges();
            },
        ]);
        #endregion 

        #region Second Fact Asserts
        Assert.Multiple([
            () => Assert.Single(SecondFact.Successes),
            () => Assert.Equal(2, SecondFact.Failures.Count),
            () => Assert.True(SecondFact.Successes[0].Pointer > 0),
            () => {
                AccountEntity Entity = SecondFact.Successes[0];
                Account Set = _source.Accounts
                    .Where(i => i.Id == Entity.Pointer)
                    .AsNoTracking()
                    .FirstOrDefault()
                    ?? throw new Exception($"Item wasn't saved correctly {nameof(SecondFact)}");

                Assert.Equal(Entity.Pointer, Set.Id);
                Assert.Equal(Entity.User, Set.User);
                Assert.True(Entity.Password.SequenceEqual(Set.Password));

                Assert.True(Entity.EvaluateSet(Set));

                _source.Remove(Set);
                _source.SaveChanges();
            },
        ]);
        #endregion 

        #region Third Fact Asserts
        Assert.Multiple([
            () => Assert.Equal(3, ThirdFact.Successes.Count),
            () => Assert.Equal(2, ThirdFact.Failures.Count),
            () => {
                Assert.All(ThirdFact.Successes, 
                    (I) => {
                        Assert.True(I.Pointer > 0);

                        Account Set = _source.Accounts
                            .Where(T => T.Id == I.Pointer)
                            .AsNoTracking()
                            .FirstOrDefault()
                            ?? throw new Exception($"Item wasn't saved correctly {nameof(ThirdFact)}({ThirdFact.Successes.IndexOf(I)})");

                        Assert.Equal(I.Pointer, Set.Id);
                        Assert.Equal(I.User, Set.User);
                        Assert.True(I.Password.SequenceEqual(Set.Password));

                        Assert.True(I.EvaluateSet(Set));

                        _source.Remove(Set);
                        _source.SaveChanges();
                });
            }
        ]);
        #endregion
    }
}
