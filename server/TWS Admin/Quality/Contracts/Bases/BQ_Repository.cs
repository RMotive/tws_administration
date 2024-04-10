using Foundation.Contracts.Datasources.Interfaces;

using Microsoft.EntityFrameworkCore;

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
    where TRepository : IRepository
    where TEntity : IEntity
    where TSet : class, ISet {

    /// <summary>
    ///     Test managed data source.
    /// </summary>
    protected readonly TLive Live;
    /// <summary>
    ///     Test managed repository implementation.
    /// </summary>
    protected readonly TRepository Repository;
    /// <summary>
    ///     Test managed collection of mocks to use.
    /// </summary>
    protected readonly TEntity[] Mocks;

    protected readonly (TSet[] Sets, TEntity[] Entities) XMocks;

    protected (TSet[] Sets, TEntity[] Entities) LiveMocks { get; private set; }

    public BQ_Repository(TLive Live, TRepository Repository) {
        this.Live = Live;
        this.Repository = Repository;
        (Mocks, XMocks) = InitMocks();
    }

    protected abstract (TEntity[] Mocks, (TSet[] Sets, TEntity[] Entities) XMocks) InitMocks();
    protected abstract Task<(TSet[] Sets, TEntity[] Entities)> InitLiveMocks();

    protected async void GenerateLiveMocks() {
        LiveMocks = await InitLiveMocks();
    }
    protected void Restore(TSet Set) {
        Live.Remove(Set);
        Live.SaveChanges();
    }
    protected void Restore(TSet[] Sets) {
        Live.Remove(Sets);
        Live.SaveChanges();
    }

    protected TSet Search(TEntity Entity, string Identifier) {
        TSet set = Live
            .Set<TSet>()
            .Where(i => i.Id == Entity.Pointer)
            .FirstOrDefault()
            ?? throw new Exception($"Item wasn't saved correctly {Identifier}");
        return set;
    }

    [Fact]
    public abstract void Create();
    [Fact]
    public abstract void Read();
    [Fact]
    public abstract void Update();
    [Fact]
    public abstract void Delete();
}
