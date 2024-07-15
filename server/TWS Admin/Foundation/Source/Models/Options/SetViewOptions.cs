namespace CSM_Foundation.Source.Models.Options;
/// <summary>
///     Defines options to build a <see cref="MigrationView"/> 
///     specifing the behavior to the builder.
/// </summary>
public class SetViewOptions {
    /// <summary>
    ///     On <see langword="true"/> indicate that the builder should consider all the new items added 
    ///     after the <see cref="Creation"/> if it is null then won't consider the limitation will behave
    ///     as this property is <see langword="true"/>
    /// </summary>
    public required bool Retroactive { get; init; }
    /// <summary>
    ///     Specifies the amount of items expected per page
    /// </summary>
    public required int Range { get; init; }
    /// <summary>
    ///     Specifies the current desired page.
    /// </summary>
    public required int Page { get; init; }
    /// <summary>
    ///     Specifies the last time this view was created, this works to limit the new entries 
    ///     on demand by <see cref="Retroactive"/>
    /// </summary>
    public DateTime? Creation { get; init; }
    /// <summary>
    ///     Indicates order actions to perform to the current view building.
    ///     <br>
    ///         The ordering actions will be performed by the array received order.
    ///     </br>
    /// </summary>
    public SetViewOrderOptions[] Orderings { get; init; } = [];
}
