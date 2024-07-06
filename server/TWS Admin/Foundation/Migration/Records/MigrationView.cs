using CSMFoundation.Migration.Interfaces;

namespace CSMFoundation.Migration.Records;

/// <summary>
///     Stores a result <see cref="MigrationView{TMigrationSet}"/> 
///     after a build operation determining paging, filtering and ordering.
/// </summary>
/// <typeparam name="TMigrationSet">
///     <typeparamref name="TMigrationSet"/> that this view handles results.
/// </typeparam>
public class MigrationView<TMigrationSet>
    where TMigrationSet : IMigrationSet {

    private TMigrationSet[] _Sets = [];
    /// <summary>
    ///     The collection of items gathered
    /// </summary>
    public required TMigrationSet[] Sets {
        get => this._Sets;
        init {
            this._Sets = value;
            this.Records = value.Length;
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
