using Foundation.Contracts.Modelling.Bases;

namespace Customer.Models;
public class ForeignSessionModel
    : BModel {
    public Guid Token { get; private set; }
    public bool Wildcard { get; private set; } = false;
    public List<int> Features { get; private set; } = [];
    public DateTime Expiration {  get; private set; } = DateTime.MinValue;

    public ForeignSessionModel() { }
    public ForeignSessionModel(Guid token, List<int> features, bool wildcard, DateTime expiration) {
        Token = token;
        Features = features;
        Wildcard = wildcard;
        Expiration = expiration;
    }
}
