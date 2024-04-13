namespace Foundation.Migrations.Interfaces;
public interface IValidator<TProperty> { 
    public bool Satisfy(Type Type);
    public void Evaluate(TProperty Property);    
}
