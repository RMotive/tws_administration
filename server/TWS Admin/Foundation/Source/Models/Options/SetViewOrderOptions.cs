using CSM_Foundation.Source.Enumerators;
using CSM_Foundation.Source.Models.Out;

namespace CSM_Foundation.Source.Models.Options;
/// <summary>
///     Stores a ordering step options for the <see cref="SetViewOut{TMigrationSet}"/>
///     builder, indicating how the current ordering step should behave.
/// </summary>
public class SetViewOrderOptions {
    /// <summary>
    ///     Property name to apply this ordering action.
    /// </summary>
    public required string Property;
    /// <summary>
    ///     Ordering behavior to apply.
    /// </summary>
    public required MIgrationViewOrderBehaviors Behavior;
}
