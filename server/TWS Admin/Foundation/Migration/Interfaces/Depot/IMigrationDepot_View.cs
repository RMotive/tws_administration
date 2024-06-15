using Foundation.Migrations.Records;

namespace Foundation.Migrations.Interfaces.Depot;
/// <summary>
///     Describes how a <see cref="IMigrationDepot_View{TMigrationSet}"/> implementation should
///     behave, providing {View} operations, a View operation is the creation of complex 
///     indexed, paged and handled TableViews based on the data.
/// </summary>
/// <typeparam name="TMigrationSet">
///     The datasource object that the implementation handles.
/// </typeparam>
public interface IMigrationDepot_View<TMigrationSet>
    where TMigrationSet : IMigrationSet {

    public Task<MigrationView<TMigrationSet>> View(MigrationViewOptions Options, Func<IQueryable<TMigrationSet>, IQueryable<TMigrationSet>>? incluide = null);
}
