using System.Text.Json.Serialization;

using CSM_Foundation.Source.Interfaces;

namespace CSM_Foundation.Source.Models;
/// <summary>
/// 
/// </summary>
public record SourceTransactionFailure {
    /// <summary>
    /// 
    /// </summary>
    public ISourceSet Set { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public string System { get; init; }
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Exception SystemInternal { get; init; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Set"></param>
    /// <param name="SystemInternal"></param>
    public SourceTransactionFailure(ISourceSet Set, Exception SystemInternal) {
        this.Set = Set;
        this.SystemInternal = SystemInternal;
        System = SystemInternal.Message;
    }
}