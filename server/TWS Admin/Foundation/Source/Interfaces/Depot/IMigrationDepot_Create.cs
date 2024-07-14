using CSM_Foundation.Source.Models.Out;

namespace CSM_Foundation.Source.Interfaces.Depot;
/// <summary>
///     Describes the behavior for a <see cref="IMigrationDepot_Create{TMigrationSet}"/>,
///     this means is able to handle a <see cref="TMigrationSet"/> live migration entity mirror between
///     standard creation transactions.
/// </summary>
/// <typeparam name="TMigrationSet">
///     Specific migration set that tthe implemented depot handles.
/// </typeparam>
public interface IMigrationDepot_Create<TMigrationSet>
    where TMigrationSet : ISourceSet {
    /// <summary>
    ///     Creates a single <paramref name="Set"/> record into the live migration.
    ///     <br>
    ///         <list type="bullet">
    ///         <listheader> NOTES: </listheader>
    ///         <item> Validates if the <paramref name="Set"/> has unique properties and validates if they already exists. </item>
    ///         <item> The <see cref="TMigrationSet.Id"/> property is always auto-generated. </item>
    ///         <item> Can auto-generate properties dependengin on the object behavior. </item>
    ///         </list>
    ///     </br>
    /// </summary>
    /// <param name="Set">
    ///     <paramref name="Set"/> to store.
    /// </param>
    /// <returns> 
    ///     The successfully stored object
    /// </returns>
    Task<TMigrationSet> Create(TMigrationSet Set);
    /// <summary>
    ///     Creates a collection of <paramref name="Sets"/> records into the live migration.
    /// </summary>
    /// <param name="Sets">
    ///     <paramref name="Sets"/> to store.
    /// </param>
    /// <param name="Sync">
    ///     If the transaction should finish at the first failure found.
    ///     throwing instantly an exception not returning the result.
    /// </param>
    /// <returns>
    ///     A record that stores the transaction result, with successes and failures collected.
    /// </returns>
    Task<SourceTransactionOut<TMigrationSet>> Create(TMigrationSet[] Sets, bool Sync = false);
}
