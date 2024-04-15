namespace Foundation.Migrations.Interfaces;
public interface IMigrationSet {
    public int Id { get; set; }

    public void Evaluate();
    public Exception[] EvaluateDefinition();
}