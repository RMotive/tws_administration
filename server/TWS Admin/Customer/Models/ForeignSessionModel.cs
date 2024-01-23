using Foundation.Contracts.Modelling.Bases;

namespace Customer.Models;
public class ForeignSessionModel
    : BModel {
    public string Token { get; private set; }
    public List<int> Features { get; private set; }

    public ForeignSessionModel(string token, List<int> features) { 
        Token = token;    
        Features = features;
    }
}
