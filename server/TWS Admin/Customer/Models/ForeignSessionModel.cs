using Foundation.Contracts.Modelling.Bases;

namespace Customer.Models;
public class ForeignSessionModel
    : BModel {
    public Guid Token { get; private set; }
    public bool Wildcard {  get; private set; } = false;
    public List<int> Features { get; private set; } = [];
    public ForeignSessionModel() { }
    public ForeignSessionModel(Guid token, List<int> features, bool isOwner) { 
        Token = token;    
        Features = features;
        Wildcard = isOwner;
    }
}
