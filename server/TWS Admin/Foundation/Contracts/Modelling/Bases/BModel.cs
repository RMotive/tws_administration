using Foundation.Contracts.Modelling.Interfaces;

namespace Foundation.Contracts.Modelling.Bases;
/// <summary>
///     A Model represent data structures validated and with operations to 
///     handle their respective business logic.
///     
///     A Model usually is a mirror from a Scheme, the Model represent the data package
///     validated from a Scheme, both bases work together to build the correct mechanism to 
///     handle data objects integrity.
/// </summary>
public abstract class BModel
    : BObject, IModel
{
}
