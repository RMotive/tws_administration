using CSMFoundation.Migration.Enumerators;
using CSMFoundation.Source.Models.Out;

namespace CSMFoundation.Source.Models.Options;
/// <summary>
///     Stores a ordering step options for the <see cref="SetViewOut{TMigrationSet}"/>
///     builder, indicating how the current ordering step should behave.
/// </summary>
public class SetViewOrderOptions {
    /// <summary>
    ///     Property name to apply this ordering action.
    /// </summary>
    required public string Property;
    /// <summary>
    ///     Ordering behavior to apply.
    /// </summary>
    required public MIgrationViewOrderBehaviors Behavior;
}
