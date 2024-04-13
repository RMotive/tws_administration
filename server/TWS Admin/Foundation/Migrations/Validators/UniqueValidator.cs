using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Validators;
public class UniqueValidator
    : IValidator<object?> {
    public void Evaluate(object? Property) { }

    public bool Satisfy(Type Type) => true;
}
