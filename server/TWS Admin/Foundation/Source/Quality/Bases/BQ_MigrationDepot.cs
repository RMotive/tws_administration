
using System.Linq.Expressions;
using System.Reflection;

using CSM_Foundation.Source.Bases;
using CSM_Foundation.Source.Enumerators;
using CSM_Foundation.Source.Interfaces;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;
using CSM_Foundation.Source.Quality.Interfaces;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace CSM_Foundation.Source.Quality.Bases;

public abstract class BQ_MigrationDepot<TMigrationSet, TMigrationDepot, TMigrationSource>
    : IQ_MigrationDepot
    where TMigrationSet : class, ISourceSet, new()
    where TMigrationDepot : IMigrationDepot<TMigrationSet>, new()
    where TMigrationSource : BSource<TMigrationSource>, new() {
    private readonly string Ordering;
    private readonly TMigrationDepot Depot;
    private readonly TMigrationSource Source;
    private readonly DbSet<TMigrationSet> Set;
    /// <summary>
    ///     Generates a new behavior base for <see cref="BQ_MigrationDepot{TMigrationSet, TMigrationDepot, TMigrationSource}"/>.
    /// </summary>
    /// <param name="Ordering">
    ///     Property name to perform <see cref="View"/> qualifications with ordering based on a property.
    /// </param>
    public BQ_MigrationDepot(string Ordering) {
        this.Ordering = Ordering;
        Depot = new();
        Source = new();
        Set = Source.Set<TMigrationSet>();
    }

    protected void Restore(ISourceSet Set) {
        _ = Source.Remove(Set);
        _ = Source.SaveChanges();
    }
    protected void Restore(ISourceSet[] Sets) {
        Source.RemoveRange(Sets);
        _ = Source.SaveChanges();
    }
    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    protected abstract TMigrationSet MockFactory();

    #region Q_Base

    [Fact]
    public async Task View() {
        #region Preparation (First-Fact) 
        TMigrationSet[] firstFactMocks = [];
        SetViewOptions firstFactOptions;
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
                    _ = await Source.SaveChangesAsync();
                }
            } catch { Restore(firstFactMocks); throw; }
        }
        #endregion
        #region Preparation (Second-Fact)
        SetViewOptions secondFactOptions;
        {
            secondFactOptions = new() {
                Page = 2,
                Range = 20,
                Retroactive = false,
            };
        }
        #endregion
        #region Preparation (Third-Fact)
        SetViewOptions thirdFactOptions;
        {
            thirdFactOptions = new() {
                Page = 1,
                Range = 20,
                Retroactive = false,
                Orderings = [
                    new SetViewOrderOptions {
                        Property = Ordering,
                        Behavior = MIgrationViewOrderBehaviors.UpDown,
                    },
                ],
            };
        }
        #endregion

        SetViewOut<TMigrationSet> firstFact = await Depot.View(firstFactOptions);
        SetViewOut<TMigrationSet> secondFact = await Depot.View(secondFactOptions);
        SetViewOut<TMigrationSet> thirdFact = await Depot.View(thirdFactOptions);

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
            #region Third-Fact (View successfuly orders by Name)
            {
                TMigrationSet[] factRecords = thirdFact.Sets;
                TMigrationSet[] sortedRecords = firstFact.Sets;

                // --> Sorting unsorted.
                {
                    Type setType = typeof(TMigrationSet);
                    ParameterExpression parameterExpression = Expression.Parameter(setType, $"X");
                    PropertyInfo property = setType.GetProperty(Ordering)
                        ?? throw new Exception($"Unexisted property ({Ordering}) on ({setType})");
                    MemberExpression memberExpression = Expression.MakeMemberAccess(parameterExpression, property);
                    UnaryExpression translationExpression = Expression.Convert(memberExpression, typeof(object));
                    Expression<Func<TMigrationSet, object>> orderingExpression = Expression.Lambda<Func<TMigrationSet, object>>(translationExpression, parameterExpression);
                    IQueryable<TMigrationSet> sorted = sortedRecords.AsQueryable();
                    sorted = sorted.OrderByDescending(orderingExpression);
                    sortedRecords = [.. sorted];
                }

                for (int i = 0; i < sortedRecords.Length; i++) {
                    TMigrationSet expected = sortedRecords[i];
                    TMigrationSet actual = factRecords[i];

                    Assert.Equal(expected.Id, actual.Id);
                }
            }
            #endregion
        } catch { throw; } finally {
            Restore(firstFactMocks);
        }
    }

    [Fact]
    public async Task Create() {
        #region First-Fact (Set successfuly saved and generated)
        {
            TMigrationSet mock = MockFactory();
            TMigrationSet fact = await Depot.Create(mock);
            Assert.Multiple([
                () => Assert.True(fact.Id > 0),
                async () => await Assert.ThrowsAnyAsync<Exception>(async () => await Depot.Create(mock)),
                () => {
                    Restore(mock);
                }
            ]);

        }
        #endregion

        #region Second-Fact (Sets successfuly saved and generated)
        {
            TMigrationSet[] mocks = [];
            for (int i = 0; i < 3; i++) {
                mocks = [.. mocks, MockFactory()];
            }
            SourceTransactionOut<TMigrationSet> fact = await Depot.Create(mocks);

            Assert.Multiple([
                () => Assert.Equal(fact.QTransactions, mocks.Length),
                () => Assert.Equal(fact.QSuccesses, mocks.Length),
                () => Assert.All(mocks, i => {
                    Assert.True(i.Id > 0);
                }),
                () => {
                    Restore(fact.Successes);
                }
            ]);
        }
        #endregion
    }

    #endregion
}
