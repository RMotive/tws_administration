using Foundation.Contracts.Datasources.Bases;
using Foundation.Contracts.Datasources.Interfaces;
using Foundation.Contracts.Exceptions;
using Foundation.Enumerators.Exceptions;

namespace Foundation.Exceptions.Datasources;
public class XSetIntegrity<TSet, TEntity>
    : BException
    where TSet : ISet
    where TEntity : IEntity {
    private readonly Type SetType;
    private readonly Type EntityType;
    private readonly BSet<TSet, TEntity> Set;
    private readonly Dictionary<string, IntegrityFailureReasons> Reasons;

    public XSetIntegrity(BSet<TSet, TEntity> Set, Dictionary<string, IntegrityFailureReasons> Reasons)
        : base($"Data handling integrity fail from: {typeof(TSet)} to: {typeof(TEntity)}") {
        SetType = typeof(TSet);
        EntityType = typeof(TEntity);
        this.Set = Set;
        this.Reasons = Reasons;
    }

    public override Dictionary<string, dynamic> ToDisplay() {
        return new Dictionary<string, dynamic>()
        {
            {"Set Type", SetType },
            {"EntityType", EntityType },
            {"Set", Set },
            {"Reasons", Reasons },
        };
    }
}
