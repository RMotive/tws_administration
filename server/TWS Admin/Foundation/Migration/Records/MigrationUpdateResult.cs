using CSMFoundation.Migration.Interfaces;

namespace CSMFoundation.Migration.Records;

/// <summary>
///     
/// </summary>
/// <typeparam name="TMigrationSet"></typeparam>
public class MigrationUpdateResult<TMigrationSet>
    where TMigrationSet : IMigrationSet {
    /// <summary>
    /// 
    /// </summary>
    required public TMigrationSet Updated { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public TMigrationSet? Previous { get; set; }
}
