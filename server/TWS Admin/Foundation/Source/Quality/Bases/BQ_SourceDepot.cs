
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

public abstract class BQ_SourceDepot<TSourceSet, TSourceDepot, TSource>
    : IQ_SourceDepot
    where TSourceSet : class, ISourceSet, new()
    where TSourceDepot : ISourceDepot<TSourceSet>, new()
    where TSource : BSource<TSource>, new() {
    private readonly string Ordering;
    private readonly TSourceDepot Depot;
    private readonly TSource Source;
    private readonly DbSet<TSourceSet> Set;
    /// <summary>
    ///     Generates a new behavior base for <see cref="BQ_SourceDepot{TMigrationSet, TMigrationDepot, TMigrationSource}"/>.
    /// </summary>
    /// <param name="Ordering">
    ///     Property name to perform <see cref="View"/> qualifications with ordering based on a property.
    /// </param>
    public BQ_SourceDepot(string Ordering) {
        this.Ordering = Ordering;
        Depot = new();
        Source = new();
        Set = Source.Set<TSourceSet>();
    }

    protected void Restore(ISourceSet Set) {
       Source.Remove(Set);
        try {
            Source.SaveChanges();
        } catch { }
    }
    protected void Restore(ISourceSet[] Sets) {
        Source.RemoveRange(Sets);
        _ = Source.SaveChanges();
    }
    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    protected abstract TSourceSet MockFactory();

    #region Q_Base

    [Fact]
    public async Task View() {
        #region Preparation (First-Fact) 
        TSourceSet[] firstFactMocks = [];
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
                        TSourceSet mock = MockFactory();
                        firstFactMocks = [.. firstFactMocks, mock];
                    }

                    await Set.AddRangeAsync(firstFactMocks);
                    await Source.SaveChangesAsync();
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

        SetViewOut<TSourceSet> firstFact = await Depot.View(firstFactOptions);
        SetViewOut<TSourceSet> secondFact = await Depot.View(secondFactOptions);
        SetViewOut<TSourceSet> thirdFact = await Depot.View(thirdFactOptions);

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
                TSourceSet[] factRecords = thirdFact.Sets;
                TSourceSet[] sortedRecords = firstFact.Sets;

                // --> Sorting unsorted.
                {
                    Type setType = typeof(TSourceSet);
                    ParameterExpression parameterExpression = Expression.Parameter(setType, $"X");
                    PropertyInfo property = setType.GetProperty(Ordering)
                        ?? throw new Exception($"Unexisted property ({Ordering}) on ({setType})");
                    MemberExpression memberExpression = Expression.MakeMemberAccess(parameterExpression, property);
                    UnaryExpression translationExpression = Expression.Convert(memberExpression, typeof(object));
                    Expression<Func<TSourceSet, object>> orderingExpression = Expression.Lambda<Func<TSourceSet, object>>(translationExpression, parameterExpression);
                    IQueryable<TSourceSet> sorted = sortedRecords.AsQueryable();
                    sorted = sorted.OrderByDescending(orderingExpression);
                    sortedRecords = [.. sorted];
                }

                for (int i = 0; i < sortedRecords.Length; i++) {
                    TSourceSet expected = sortedRecords[i];
                    TSourceSet actual = factRecords[i];

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
            TSourceSet mock = MockFactory();
            TSourceSet fact = await Depot.Create(mock);
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
            TSourceSet[] mocks = [];
            for (int i = 0; i < 3; i++) {
                mocks = [.. mocks, MockFactory()];
            }
            SourceTransactionOut<TSourceSet> fact = await Depot.Create(mocks);

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


    [Fact]
    public async Task Delete() {
        #region First fact (Set removed correctly by id)
        {
            TSourceSet sourceMock = MockFactory();
            await Set.AddAsync(sourceMock);
            await Source.SaveChangesAsync();

            TSourceSet factResult = await Depot.Delete(sourceMock.Id);

            Assert.Multiple([
                () => Assert.True(sourceMock.Id > 0),
                () => Assert.True(sourceMock.Id == factResult.Id),
                () => {
                    TSourceSet? searchResult = Set.Where(i => i.Id == sourceMock.Id).FirstOrDefault();
                    Assert.Null(searchResult);
                },
                () => {
                    Restore(sourceMock);
                }
            ]);
        }
        #endregion
    }

    #endregion
}
