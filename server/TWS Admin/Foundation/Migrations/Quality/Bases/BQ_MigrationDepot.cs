using Foundation.Migrations.Bases;
using Foundation.Migrations.Interfaces;
using Foundation.Migrations.Quality.Interfaces;
using Foundation.Migrations.Records;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Foundation.Migrations.Quality.Bases;

public abstract class BQ_MigrationDepot<TMigrationSet, TMigrationDepot, TMigrationSource>
    : IQ_MigrationDepot
    where TMigrationSet : class, IMigrationSet, new()
    where TMigrationDepot : IMigrationDepot<TMigrationSet>, new()
    where TMigrationSource : BMigrationSource<TMigrationSource>, new() {


    readonly TMigrationDepot Depot;
    readonly TMigrationSource Source;
    readonly DbSet<TMigrationSet> Set;
    public BQ_MigrationDepot() {
        Depot = new();
        Source = new();
        Set = Source.Set<TMigrationSet>();
    }

    protected void Restore(IMigrationSet Set) {
        Source.Remove(Set);
        Source.SaveChanges();
    }
    protected void Restore(IMigrationSet[] Sets) {
        Source.RemoveRange(Sets);
        Source.SaveChanges();
    }
    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    protected abstract TMigrationSet MockFactory();

    #region Q_Base

    [Fact]
    public async void View() {
        #region Preparation (First-Fact) 
        TMigrationSet[] firstFactMocks = [];
        MigrationViewOptions firstFactOptions;
        {
            try {
                firstFactOptions = new() {
                    Retroactive = false,
                    Range = 20,
                    Page = 1,
                };
                int stored = Set.Count();
                if (stored < 21) {
                    int left = 21 - stored;
                    for (int i = 0; i < left; i++) {
                        TMigrationSet mock = MockFactory();
                        firstFactMocks = [.. firstFactMocks, mock];
                    }

                    await Set.AddRangeAsync(firstFactMocks);
                    await Source.SaveChangesAsync();
                }
            } catch { Restore(firstFactMocks); throw; }
        }
        #endregion
        #region Preparation (Second-Fact)
        MigrationViewOptions secondFactOptions;
        {
            secondFactOptions = new() {
                Page = 2,
                Range = 20,
                Retroactive = false,
            };
        }
        #endregion

        MigrationView<TMigrationSet> firstFact = await Depot.View(firstFactOptions);
        MigrationView<TMigrationSet> secondFact = await Depot.View(secondFactOptions);

        try {
            #region First-Fact (View successfully page 1)
            {
                Assert.Multiple(
                    () => Assert.True(firstFact.Pages > 1),
                    () => Assert.True(firstFact.Records > 0),
                    () => Assert.Equal(firstFactOptions.Page, firstFact.Page),
                    () => Assert.Equal(firstFact.Records, firstFact.Sets.Length)
                );
            }
            #endregion
            #region Second-Fact (View successfully page 2)
            {
                Assert.Multiple(
                    () => Assert.True(secondFact.Pages > 1),
                    () => Assert.True(secondFact.Records > 0),
                    () => Assert.Equal(secondFactOptions.Page, secondFact.Page),
                    () => Assert.Equal(secondFact.Records, secondFact.Sets.Length)
                );
            }
            #endregion
        } catch { throw; } finally {
            Restore(firstFactMocks);
        }
    }

    [Fact]
    public async void Create() {
        TMigrationSet mock = MockFactory();

        TMigrationSet firstFact = await Depot.Create(mock);

        #region First-Fact (Set successfully saved and generated)
        {
            Assert.Multiple([
                    () => Assert.True(firstFact.Id > 0),
                async () => await Assert.ThrowsAnyAsync<Exception>(async () => await Depot.Create(mock)),
                () => {
                    Restore(mock);
                }
            ]);

        }
        #endregion
    }

    #endregion
}
