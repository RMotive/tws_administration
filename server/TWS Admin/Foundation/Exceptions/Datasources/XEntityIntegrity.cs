using Foundation.Contracts.Datasources.Bases;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Datasources;
public class XEntityIntegrity<TSet, TEntity>
    : BException
    where TSet : ISet
    where TEntity : IEntity {
    private readonly BEntity<TSet, TEntity> Entity;
    private readonly Type EntityType;
    private readonly Type SetType;
    private readonly Dictionary<string, IntegrityFailureReasons> Reasons;

    public XEntityIntegrity(BEntity<TSet, TEntity> Entity, Dictionary<string, IntegrityFailureReasons> Reasons)
        : base($"Data handling integrity fail from: {typeof(TEntity)} to: {typeof(TSet)}") {
        this.Entity = Entity;
        this.Reasons = Reasons;
        EntityType = typeof(TEntity);
        SetType = typeof(TEntity);
    }

    public override Dictionary<string, dynamic> GenerateAdvising()
    => new() {
        {nameof(Entity), Entity},
        {nameof(EntityType), EntityType},
        {nameof(SetType), SetType},
        {nameof(Reasons), Reasons},
    };
}
