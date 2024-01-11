using System.Reflection;
using System.Text.Json;

namespace Foundation.Contracts.Modelling.Bases;

/// <summary>
///     Represents an inheritance link between datasource objects
///     that need specific equality comparisson between their properties.
/// </summary>
public abstract class BObject {
    public override bool Equals(object? Comparer) {
        if (this is null && Comparer is null) return true;
        if (this is not null && Comparer is null) return false;
        if (this is null && Comparer is not null) return false;
        if (GetType() != Comparer?.GetType()) return false;

        PropertyInfo[] rProperties = GetType().GetProperties();
        PropertyInfo[] cProperties = Comparer.GetType().GetProperties();

        for (int i = 0; i < rProperties.Length; i++) {
            PropertyInfo rProperty = rProperties[i];
            PropertyInfo cProperty = cProperties[i];

            if (rProperty.Name != cProperty.Name) return false;
            if (rProperty.PropertyType != cProperty.PropertyType) return false;
            dynamic? rReferenceValue = rProperty.GetValue(this);
            dynamic? cReferenceValue = cProperty.GetValue(Comparer);

            #region Specific struct types equality validations.
            bool ReferenceValuesNotNull = rReferenceValue != null && cReferenceValue != null;
            
            #region Byte Array Equality
            bool isByteArray = rProperty.PropertyType == typeof(byte[]);
            if(isByteArray && ReferenceValuesNotNull) {
                byte[] currentReferenceValue = (byte[])rReferenceValue!;
                byte[] comparerReferenceValue = (byte[])cReferenceValue!;
                return currentReferenceValue.SequenceEqual(comparerReferenceValue);
            }
            #endregion

            #endregion

            if (rReferenceValue != cReferenceValue) return false;
        }
        return true;
    }

    public override string ToString() {
        Dictionary<string, dynamic?> jsonReference = [];
        PropertyInfo[] propReferences = GetType().GetProperties();
        foreach (PropertyInfo prop in propReferences) {
            jsonReference.Add(prop.Name, prop.GetValue(this));
        }

        return JsonSerializer.Serialize(jsonReference);
    }
    public override int GetHashCode() => GetHashCode();
}
