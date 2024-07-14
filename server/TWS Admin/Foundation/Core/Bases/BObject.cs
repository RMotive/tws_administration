using System.Reflection;
using System.Text.Json;

namespace CSM_Foundation.Core.Bases;

/// <summary>
///     Represents an inheritance link between datasource objects
///     that need specific equality comparisson between their properties.
/// </summary>
public abstract class BObject<TObject> {
    protected virtual PropertyInfo[] EqualityExceptions() {
        return [];
    }

    public override bool Equals(object? Comparer) {
        PropertyInfo[] exceptions = EqualityExceptions();

        if (this is null && Comparer is null) {
            return true;
        }

        if (this is not null && Comparer is null) {
            return false;
        }

        if (this is null && Comparer is not null) {
            return false;
        }

        if (GetType() != Comparer?.GetType()) {
            return false;
        }

        PropertyInfo[] rProperties = GetType().GetProperties();
        PropertyInfo[] cProperties = Comparer.GetType().GetProperties();

        for (int i = 0; i < rProperties.Length; i++) {
            PropertyInfo rProperty = rProperties[i];
            PropertyInfo cProperty = cProperties[i];

            if (exceptions.Contains(rProperty) || exceptions.Contains(cProperty)) {
                continue;
            }

            if (rProperty.Name != cProperty.Name) {
                return false;
            }

            if (rProperty.PropertyType != cProperty.PropertyType) {
                return false;
            }

            dynamic? rReferenceValue = rProperty.GetValue(this);
            dynamic? cReferenceValue = cProperty.GetValue(Comparer);

            #region Specific struct types equality validations.
            bool ReferenceValuesNotNull = rReferenceValue != null && cReferenceValue != null;

            #region Byte Array Equality
            bool isByteArray = rProperty.PropertyType == typeof(byte[]);
            if (isByteArray && ReferenceValuesNotNull) {
                byte[] currentReferenceValue = (byte[])rReferenceValue!;
                byte[] comparerReferenceValue = (byte[])cReferenceValue!;
                bool comparisson = currentReferenceValue.SequenceEqual(comparerReferenceValue);
                if (comparisson) {
                    continue;
                }

                return false;
            }
            #endregion

            #endregion

            if (rReferenceValue != cReferenceValue) {
                return false;
            }
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
    public override int GetHashCode() {
        return base.GetHashCode();
    }

    /// <summary>
    ///     Localizes the current object Property mirror reflected.
    /// </summary>
    /// <param name="name">
    ///     Name of the property to localize.
    /// </param>
    /// <returns>
    ///     Property reflected info.
    /// </returns>
    /// <exception cref="XGetProperty"> 
    ///     If the Property wasn't find.
    /// </exception>
    public PropertyInfo GetProperty(string name) {
        Type reflection = GetType();
        PropertyInfo tracedProperty
        = reflection.GetProperty(name)
            ?? throw new Exception(""); //TODO: REPLACE THIS

        return tracedProperty;
    }
    public TObject Clone() {
        Type ObjectType = typeof(TObject);

        TObject? Cloned = (TObject?)Activator.CreateInstance(ObjectType)
            ?? throw new Exception("PENDING WRONG ACTIVATION");
        PropertyInfo[] ObjectPropertiesInfo = ObjectType.GetProperties();
        foreach (PropertyInfo PropertyInfo in ObjectPropertiesInfo) {
            object? OriginalValue = PropertyInfo.GetValue(this);
            PropertyInfo.SetValue(Cloned, OriginalValue);
        }

        return Cloned.GetHashCode() == GetHashCode() ? throw new Exception("PEDNING SAME HASH") : Cloned;
    }
}
