namespace Foundation.Migrations.Interfaces;
public interface IMigrationSet {
    public void Evaluate(bool Definition = false);
}