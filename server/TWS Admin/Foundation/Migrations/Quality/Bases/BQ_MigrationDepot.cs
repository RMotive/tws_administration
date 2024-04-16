using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Quality.Interfaces;

using Xunit;

namespace Foundation.Migrations.Quality.Bases;

public abstract class BQ_MigrationDepot<TMigrationSet, TMigrationDepot, TMigrationSource>
    : IQ_MigrationDepot
    where TMigrationSet : IMigrationSet, new()
    where TMigrationDepot : IMigrationDepot<TMigrationSet>, new()
    where TMigrationSource : BMigrationSource<TMigrationSource>, new() {

    
    readonly TMigrationDepot Depot;
    readonly TMigrationSource Source;
    public BQ_MigrationDepot() {
        Depot = new();
        Source = new();
    }

    protected void Restore(IMigrationSet Set) {
        Source.Remove(Set);
        Source.SaveChanges();   
    }
    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    protected abstract TMigrationSet MockFactory();

    [Fact]
    public async void Create() {
        TMigrationSet mock = MockFactory();

        TMigrationSet firstFact = await Depot.Create(mock);

        #region First-Fact (Set successfully saved and generated)

        Assert.Multiple([
                () => Assert.True(firstFact.Id > 0),
                async () => await Assert.ThrowsAnyAsync<Exception>(async () => await  Depot.Create(mock)),
                () => {
                    Restore(mock);
                }
            ]);

        #endregion
    }
}
