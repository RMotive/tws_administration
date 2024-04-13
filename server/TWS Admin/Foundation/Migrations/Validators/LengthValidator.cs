using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Validators;
public class LengthValidator 
    : IValidator<object?> {

    private readonly int? Min;
    private readonly int? Max;

    public LengthValidator(int? Min = null, int? Max = null) {
        this.Min = Min;
        this.Max = Max;
    }

    public bool Satisfy(Type Type) {
        if(Type == typeof(string)) return true;
        if(Type == typeof(IList<>)) return true;
        if(Type == typeof(IEnumerable<>)) return true;
        if(Type == typeof(ICollection<>)) return true;
        if(Type.IsArray) return true;

        return false;
    }

    public void Evaluate(object? Property) {
        if(Property is null) return;
        if(!Satisfy(Property.GetType())) return;

        switch(Property) {
            case string value:
                if(Min != null && value.Length < Min) throw new Exception("Min value");
                if(Max != null && value.Length > Max) throw new Exception("Max Value");
                break;
            default:
                throw new Exception("Wrong");
        };
    }
}
