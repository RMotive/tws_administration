using Foundation.Contracts.Datasources.Bases;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Datasources;
public class XSetIntegrity<TSet, TEntity>
    : BException
    where TSet : ISet
    where TEntity : IEntity {
    public readonly Type SetType;
    public readonly Type EntityType;
    public readonly BSet<TSet, TEntity> Set;
    public readonly Dictionary<string, IntegrityFailureReasons> Reasons;

    public XSetIntegrity(BSet<TSet, TEntity> Set, Dictionary<string, IntegrityFailureReasons> Reasons)
        : base($"Data handling integrity fail from: {typeof(TSet)} to: {typeof(TEntity)}") {
        SetType = typeof(TSet);
        EntityType = typeof(TEntity);
        this.Set = Set;
        this.Reasons = Reasons;
    }
}
