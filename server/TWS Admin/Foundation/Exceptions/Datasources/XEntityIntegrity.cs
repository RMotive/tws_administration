using Foundation.Contracts.Datasources.Bases;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Datasources;
public class XEntityIntegrity<TSet, TEntity>
    : BException
    where TSet : ISet
    where TEntity : IEntity {
    public readonly BEntity<TSet, TEntity> Entity;
    public readonly Type EntityType;
    public readonly Type SetType;
    public readonly Dictionary<string, IntegrityFailureReasons> Reasons;

    public XEntityIntegrity(BEntity<TSet, TEntity> Entity, Dictionary<string, IntegrityFailureReasons> Reasons)
        : base($"Data handling integrity fail from: {typeof(TEntity)} to: {typeof(TSet)}") {
        this.Entity = Entity;
        this.EntityType = typeof(TEntity);
        this.SetType = typeof(TEntity);
        this.Reasons = Reasons;
    }
}
