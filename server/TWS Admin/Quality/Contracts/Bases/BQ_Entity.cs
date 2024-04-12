using Foundation.Contracts.Datasources.Interfaces;

using TWS_Security.Quality.Contracts.Interfaces;

using Xunit;

namespace TWS_Security.Quality.Contracts.Bases;
/// <summary>
///     Base for [Q_Entity].
///     
///     Defines useful behaviors to a [Q_Entity] implementation.
///     
///     [Q_Entity] concept: a quality class to evaluate Entity concept implementations.
/// </summary>
public abstract class BQ_Entity<TSet, TEntity>
    : IQ_Set
    where TSet : ISet
    where TEntity : IEntity {
    /// <summary>
    ///     A test managed <see cref="TSet"/> mocked instance.
    /// </summary>
    protected TSet SetMock { get; private set; } = default!;
    /// <summary>
    ///     A test managed <see cref="TEntity"/> mocked instance
    /// </summary>
    protected TEntity EntityMock { get; private set; } = default!;
    /// <summary>
    ///     A test method <see cref="TEntity"/> mocked empty instance (invalid instance).
    /// </summary>
    protected TEntity XEntityMock { get; private set; } = default!;

    protected BQ_Entity() {
        (SetMock, EntityMock, XEntityMock) = InitMocks();
    }

    /// <summary>
    ///     Initializes the basse mocks to perform a <see cref="BQ_Set{TSet, TEntity}"/> quality check.
    /// </summary>
    /// <returns></returns>
    abstract protected (TSet SetMock, TEntity EntityMock, TEntity XEntityMock) InitMocks();

    [Fact]
    public abstract void BuildSet();
    [Fact]
    public abstract void EvaluateSet();
}
