using System.Text.Json.Serialization;

using CSMFoundation.Migration.Interfaces;

namespace CSMFoundation.Migration.Records;
public record MigrationTransactionFailure {
    public IMigrationSet Set { get; init; }

    public string System { get; init; }

    [JsonIgnore]
    public Exception SystemInternal { get; init; }

    public MigrationTransactionFailure(IMigrationSet Set, Exception SystemInternal) {
        this.Set = Set;
        this.SystemInternal = SystemInternal;
        this.System = SystemInternal.Message;
    }
}