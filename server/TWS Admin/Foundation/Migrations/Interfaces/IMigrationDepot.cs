using Foundation.Migrations.Interfaces.Depot;

namespace Foundation.Migrations.Interfaces;
public interface IMigrationDepot<TMigrationSet>
    : IMigrationDepot_Create<TMigrationSet>
    where TMigrationSet : IMigrationSet { }
