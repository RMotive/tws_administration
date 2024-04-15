using System.Reflection;

namespace Foundation.Migrations.Interfaces;
/// <summary>
/// TODO
/// </summary>
public interface IValidator {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public bool Satisfy(Type Type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Property"></param>
    /// <param name="Value"></param>
    public void Evaluate(PropertyInfo Property, object? Value);
}
