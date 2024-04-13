using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Foundation.Contracts.Modelling.Bases;
using Foundation.Migrations.Exceptions;
using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationSet
    : BObject<IMigrationSet>, IMigrationSet {
    private static (string Property, IValidator<object?>[] Validations)[]? Evaluations { get; set; } 
    private static bool Defined {  get; set; }

    protected abstract (string Property, IValidator<object?>[])[] Validations((string Property, IValidator<object?>[])[] Container);
    public void Evaluate(bool Definition = false) {
        Evaluations ??= Validations([]);
        IEnumerable<string> toEvaluate = Evaluations
            .Select(i => i.Property);
        if(!Defined) {
            // Checking if the validations defintions aren't duplicated.
            IEnumerable<string> duplicated = toEvaluate
                .GroupBy(i => i)
                .Where(g => g.Count() > 1)
                .Select(i => i.Key);
            if (duplicated.Any()) {

                throw new XValidationsDefinition(duplicated, XValidationsDefinition.Reasons.Duplication, GetType());
            }

            // Checking if all the validations defined properties exist in the class definition.
            PropertyInfo[] properties = GetType().GetProperties();
            IEnumerable<string> existProperties = properties
                .Select(i => i.Name);
            IEnumerable<string> Unexist = toEvaluate.Except(existProperties);
            if (Unexist.Any()) {
                throw new XValidationsDefinition(Unexist, XValidationsDefinition.Reasons.Unexist, GetType());
            }

            Defined = true;
            if (Definition) return;
        }

        if(Definition) return;

        int quantity = Evaluations.Length;
        for (int i = 0; i < quantity; i++) {
            (string property, IValidator<object?>[] validations) = Evaluations[i];
            PropertyInfo pi = Property(property);

            object? value = pi.GetValue(this);
            foreach(IValidator<object?> validator in validations) {
                validator.Evaluate(value);
            }
        }
    }
}
