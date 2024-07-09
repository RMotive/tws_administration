namespace CSMFoundation.Migration.Interfaces;
public interface ISourceSet {
    public int Id { get; set; }

    public void EvaluateRead();
    public void EvaluateWrite();
    public Exception[] EvaluateDefinition();
}