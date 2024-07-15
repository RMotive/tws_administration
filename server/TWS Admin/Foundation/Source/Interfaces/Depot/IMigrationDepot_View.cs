using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

namespace CSM_Foundation.Source.Interfaces.Depot;
/// <summary>
///     Describes how a <see cref="IMigrationDepot_View{TMigrationSet}"/> implementation should
///     behave, providing {View} operations, a View operation is the creation of complex 
///     indexed, paged and handled TableViews based on the data.
/// </summary>
/// <typeparam name="TMigrationSet">
///     The datasource object that the implementation handles.
/// </typeparam>
public interface IMigrationDepot_View<TMigrationSet>
    where TMigrationSet : ISourceSet {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Options"></param>
    /// <param name="Include"></param>
    /// <returns></returns>
    Task<SetViewOut<TMigrationSet>> View(SetViewOptions Options, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? Include = null);
}
