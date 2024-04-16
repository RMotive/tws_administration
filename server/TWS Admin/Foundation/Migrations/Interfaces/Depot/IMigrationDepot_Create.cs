namespace Foundation.Migrations.Interfaces.Depot;

public interface IMigrationDepot_Create<TMigrationSet>
    where TMigrationSet : IMigrationSet {
    public Task<TMigrationSet> Create(TMigrationSet Set);
}
