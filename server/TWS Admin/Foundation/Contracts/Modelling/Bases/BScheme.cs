using System.Reflection;
using Foundation.Contracts.Modelling.Interfaces;
using Foundation.Contracts.Records;
using Foundation.Exceptions.Modelling;
using Foundation.Models;

namespace Foundation.Contracts.Modelling.Bases;
/// <summary>
///     A Base class to be inherited by classes that are Schemes.
///     
///     A Scheme represent a raw model abstraction that represents data models 
///     retrieved/gathered from external datasources that that can't be validated on 
///     objects building, is just a representation of a json raw data package.
/// </summary>
public abstract class BScheme<TScheme, TModel>
    : BObject<IModel>, 
        IScheme<TModel>
    where TModel : IModel
    where TScheme : IScheme<TModel> {
    protected const string NULL_EMPTY_REASON = "is null or empty";        


    private readonly List<IValidationRule> Rules;
    
    protected BScheme() {
        // --> Rules get generated at the object is buildt to don't regenerate them each Generation request.
        Rules = GenerateRules();
    }

    protected virtual List<IValidationRule> GenerateRules() => [];
    protected abstract TModel Generate();

    public TModel GenerateModel() {
        List<SchemeConvertionBreakModel> breaks = [];

        PropertyInfo[] schemeProperties = GetType().GetProperties();
        foreach(PropertyInfo prop in schemeProperties) {
             
            SchemeConvertionBreakModel? @break = null;
            IValidationRule? Rule = Rules.Find(I => I.Property.Name == prop.Name);
            if(Rule is null)
                continue;
            Type specifiedPropertyType = Rule.SpecifiedType;
            Type actualPropertyType = prop.PropertyType;

            // --> At this point we know that the property has a integrity rule to validate.
            if(specifiedPropertyType != actualPropertyType) 
                @break = new SchemeConvertionBreakModel(prop.Name, $"reflection issue rule({specifiedPropertyType}), actual({actualPropertyType})");
            
            object? objectReflectedValue = prop.GetValue(this);
            var castedReflectedValue = Convert.ChangeType(objectReflectedValue, Rule.SpecifiedType);
            string? validator = Rule.ValidateRule(castedReflectedValue);
            if(validator != null) 
                @break = new SchemeConvertionBreakModel(prop.Name, validator);
            if(@break is null)
                continue;
            breaks.Add(@break);
        }

        if(breaks.Count > 0)
            throw new XGenerateModel<TScheme, TModel>(breaks);
        return Generate();
    }
}
