using CSM_Foundation.Source.Interfaces.Depot;

namespace CSM_Foundation.Source.Interfaces;
/// <summary>
///     Determines how a complex <see cref="ISourceDepot{TMigrationSet}"/> 
///     implementation should behave.
///     
///     <br>
///         A <see cref="ISourceDepot{TMigrationSet}"/> is considered as a
///         data repository for a specific <typeparamref name="TMigrationSet"/>.
///         providing data, storing data, updating data, etc...
///     </br>
/// </summary>
/// <typeparam name="TMigrationSet">
///     The datasource object that the implementation handles.
/// </typeparam>
public interface ISourceDepot<TMigrationSet>
    : IMigrationDepot_View<TMigrationSet>
    , IMigrationDepot_Read<TMigrationSet>
    , IMigrationDepot_Create<TMigrationSet>
    , IMigrationDepot_Update<TMigrationSet>
    , ISourceDepot_Delete<TMigrationSet>
    where TMigrationSet : ISourceSet { }
