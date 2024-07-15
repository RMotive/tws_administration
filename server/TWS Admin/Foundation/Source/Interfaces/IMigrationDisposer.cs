using Microsoft.EntityFrameworkCore;

namespace CSM_Foundation.Source.Interfaces;
/// <summary>
/// 
/// </summary>
public interface IMigrationDisposer {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Set"></param>
    void Push(DbContext Source, ISourceSet Set);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Sets"></param>
    void Push(DbContext Source, ISourceSet[] Sets);
    /// <summary>
    /// 
    /// </summary>
    void Dispose();
}
