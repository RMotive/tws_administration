using CSMFoundation.Migration.Records;

namespace CSMFoundation.Migration.Interfaces.Depot;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TMigrationSet"></typeparam>
public interface IMigrationDepot_Update<TMigrationSet>
    where TMigrationSet : IMigrationSet {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Set"></param>
    /// <returns></returns>
    Task<MigrationUpdateResult<TMigrationSet>> Update(TMigrationSet Set);
}
