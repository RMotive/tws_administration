using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Validators;
public class RequiredValidator 
    : IValidator<object?> {
    public RequiredValidator() { }
    public void Evaluate(object? Property) {
        if(Property is not null) return;
        
        throw new Exception("The field is required but the value came null");
    }

    public bool Satisfy(Type Type) => true;
}
