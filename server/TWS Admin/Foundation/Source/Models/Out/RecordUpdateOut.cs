using CSMFoundation.Migration.Interfaces;

namespace CSMFoundation.Source.Models.Out;

/// <summary>
///     
/// </summary>
/// <typeparam name="TMigrationSet"></typeparam>
public class RecordUpdateOut<TMigrationSet>
    where TMigrationSet : ISourceSet {
    /// <summary>
    /// 
    /// </summary>
    required public TMigrationSet Updated { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public TMigrationSet? Previous { get; set; }
}
