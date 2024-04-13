using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Foundation.Migrations.Interfaces;

namespace Foundation.Migrations.Bases;
public abstract class BMigrationEntity<TSet> 
    : IMigrationEntity<TSet> 
    where TSet : class, new() {
    public int Pointer { get; set; }


    protected abstract TSet Migrate();
    public TSet Evaluate() {
        PropertyInfo[] propertie = GetType().GetProperties();
        foreach (PropertyInfo property in propertie) {
            foreach(CustomAttributeData attribute in property.CustomAttributes) {
                if(attribute.AttributeType != typeof(ValidationAttribute)) continue;


            }
        }

        return Migrate();
    }
}
