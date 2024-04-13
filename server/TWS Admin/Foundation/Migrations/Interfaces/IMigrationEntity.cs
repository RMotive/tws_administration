namespace Foundation.Migrations.Interfaces;
public interface IMigrationEntity<TSet> 
    : IMigrationEntity 
    where TSet: class, new() {

    public TSet Evaluate();
}

public interface IMigrationEntity {
    public int Pointer { get; set; }
}
