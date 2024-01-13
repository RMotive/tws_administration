using Foundation.Contracts.Modelling.Interfaces;

namespace Foundation.Contracts.Modelling.Bases;
/// <summary>
///     A Base class to be inherited by classes that are Schemes.
///     
///     A Scheme represent a raw model abstraction that represents data models 
///     retrieved/gathered from external datasources that that can't be validated on 
///     objects building, is just a representation of a json raw data package.
/// </summary>
public abstract class BScheme<TModel>
    : BObject<IModel>, 
        IScheme<TModel>
    where TModel : IModel {
    public abstract TModel GenerateModel();
}
