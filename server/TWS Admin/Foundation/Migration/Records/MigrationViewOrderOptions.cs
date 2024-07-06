using CSMFoundation.Migration.Enumerators;

namespace CSMFoundation.Migration.Records;
/// <summary>
///     Stores a ordering step options for the <see cref="MigrationView{TMigrationSet}"/>
///     builder, indicating how the current ordering step should behave.
/// </summary>
public class MigrationViewOrderOptions {
    /// <summary>
    ///     Property name to apply this ordering action.
    /// </summary>
    required public string Property;
    /// <summary>
    ///     Ordering behavior to apply.
    /// </summary>
    required public MIgrationViewOrderBehaviors Behavior;
}
