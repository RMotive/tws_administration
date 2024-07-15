using CSM_Foundation.Source.Interfaces;

namespace CSM_Foundation.Source.Models.Out;
public record SourceTransactionOut<TSet>
    where TSet : ISourceSet {
    public TSet[] Successes { get; init; }
    public SourceTransactionFailure[] Failures { get; init; }
    public int QTransactions { get; private set; }
    public int QSuccesses { get; private set; }
    public int QFailures { get; private set; }
    public bool Failed { get; private set; }

    public SourceTransactionOut(TSet[] Successes, SourceTransactionFailure[] Failures) {
        this.Successes = Successes;
        this.Failures = Failures;
        QSuccesses = this.Successes.Length;
        QFailures = this.Failures.Length;
        QTransactions = QSuccesses + QFailures;
        Failed = QFailures > 0;
    }
}