using Customer.Contracts.Models;

using Foundation.Contracts.Modelling.Bases;

namespace Customer.Models;

public class SessionModel 
    : BModel, IPrivateModel<ForeignSessionModel> {
    public Guid Token { get; private set; }
    public bool Wildcard {  get; private set; }
    public AccountIdentityModel Identity { get; private set; }
    public DateTime GenerationTimemark { get; private set; }
    public DateTime UpdateTimemark { get; private set; }
    public DateTime ExpirationTimemark {  get; private set; }
    public TimeSpan ExpirationTimemarkThreshold { get; private set; }
    public List<int> Features {  get; private set; }

    public SessionModel(Guid token, bool isOwner, AccountIdentityModel identity, TimeSpan threshold, List<int> features) { 
        Token = token;
        Identity = identity;
        GenerationTimemark = DateTime.UtcNow;
        UpdateTimemark = GenerationTimemark;
        ExpirationTimemark = GenerationTimemark.Add(threshold); 
        ExpirationTimemarkThreshold = threshold;
        Features = features;
        Wildcard = isOwner; 
    }

    public ForeignSessionModel GeneratePublicDerivation() 
    => new(Token, Features, Wildcard);
}
