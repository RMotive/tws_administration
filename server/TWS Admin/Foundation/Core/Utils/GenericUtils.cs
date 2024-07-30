
using System.Collections;
using System.Reflection;

namespace CSM_Foundation.Core.Utils;
public static class GenericUtils {
    public static TObject DeepCopy<TObject>(this TObject target, bool isParentProperty = true, Dictionary<object, object>? visited = null) {
        visited ??= new Dictionary<object, object>();

        if (target == null)
            return default!;

        Type ObjectType = isParentProperty ? typeof(TObject) : target.GetType();

        if (visited.TryGetValue(target, out var existingClone))
            return (TObject)existingClone;

        TObject Cloned = (TObject?)Activator.CreateInstance(ObjectType)
            ?? throw new Exception("PENDING WRONG ACTIVATION");

        visited[target] = Cloned;

        PropertyInfo[] ObjectPropertiesInfo = ObjectType.GetProperties();
        foreach (PropertyInfo PropertyInfo in ObjectPropertiesInfo) {
            if (!PropertyInfo.CanRead || !PropertyInfo.CanWrite || PropertyInfo.GetIndexParameters().Length > 0)
                continue;

            object? OriginalValue = PropertyInfo.GetValue(target);
            if (OriginalValue == null) {
                PropertyInfo.SetValue(Cloned, null);
            } else if (PropertyInfo.PropertyType.IsValueType || PropertyInfo.PropertyType == typeof(string)) {
                PropertyInfo.SetValue(Cloned, OriginalValue);
            } else if (typeof(IEnumerable).IsAssignableFrom(PropertyInfo.PropertyType)) {
                var clonedCollection = CloneCollection((IEnumerable)OriginalValue, PropertyInfo.PropertyType, visited);
                PropertyInfo.SetValue(Cloned, clonedCollection);
            } else {
                var clonedValue = OriginalValue.DeepCopy(false, visited);
                PropertyInfo.SetValue(Cloned, clonedValue);
            }
        }

        return Cloned.GetHashCode() == target!.GetHashCode()
            ? throw new Exception("Clonation Error:\n SAME HASH ON:" + target.GetType())
            : Cloned;
    }

    private static object CloneCollection(IEnumerable originalCollection, Type collectionType, Dictionary<object, object> visited) {
        Type elementType = collectionType.IsGenericType
            ? collectionType.GetGenericArguments()[0]
            : typeof(object);

        var clonedCollection = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
        if (clonedCollection == null) return originalCollection;

        foreach (var item in originalCollection) {
            var clonedItem = item.DeepCopy(false, visited);
            clonedCollection.Add(clonedItem);
        }

        return clonedCollection;
    }
}
