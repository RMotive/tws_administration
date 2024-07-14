using CSM_Foundation.Source.Interfaces;

namespace CSM_Foundation.Source.Models.Out;

/// <summary>
///     Stores a result <see cref="SetViewOut{TMigrationSet}"/> 
///     after a build operation determining paging, filtering and ordering.
/// </summary>
/// <typeparam name="TMigrationSet">
///     <typeparamref name="TMigrationSet"/> that this view handles results.
/// </typeparam>
public class SetViewOut<TMigrationSet>
    where TMigrationSet : ISourceSet {

    private TMigrationSet[] _Sets = [];
    /// <summary>
    ///     The collection of items gathered
    /// </summary>
    public required TMigrationSet[] Sets {
        get => _Sets;
        init {
            _Sets = value;
            Records = value.Length;
        }
    }
    /// <summary>
    ///     The available pages.
    /// </summary>
    public required int Pages { get; init; }
    /// <summary>
    ///     The current page.
    /// </summary>
    public required int Page { get; init; }
    /// <summary>
    ///     Indicates the timemark of time when was created.
    /// </summary>
    public DateTime Creation { get; init; } = DateTime.UtcNow;
    /// <summary>
    ///     Indicates the amount of records that this result contains.
    /// </summary>
    public int Records { get; init; }
    /// <summary>
    ///     Amount of total records that currently exist at the live database
    /// </summary>
    public required int Amount { get; init; }
}
