using CSMFoundation.Migration.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace CSMFoundation.Migration.Interfaces;
/// <summary>
/// 
/// </summary>
public interface IMigrationDisposer {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Set"></param>
    void Push(DbContext Source, IMigrationSet Set);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Sets"></param>
    void Push(DbContext Source, IMigrationSet[] Sets);
    /// <summary>
    /// 
    /// </summary>
    void Dispose();
}
