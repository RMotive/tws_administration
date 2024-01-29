using Foundation.Contracts.Modelling.Bases;

namespace Customer.Models;
public class SessionModel 
    : BModel {

    public string Token { get; private set; }
    public AccountIdentityModel Identity { get; private set; }
    public DateTime GenerationTimemark { get; private set; }
    public DateTime UpdateTimemark { get; private set; }
    public DateTime ExpirationTimemark {  get; private set; }
    public TimeSpan ExpirationTimemarkThreshold { get; private set; }

    public SessionModel(string token, AccountIdentityModel identity, TimeSpan threshold) { 
        Token = token;
        Identity = identity;
        GenerationTimemark = DateTime.UtcNow;
        UpdateTimemark = GenerationTimemark;
        ExpirationTimemark = GenerationTimemark.Add(threshold); 
        ExpirationTimemarkThreshold = threshold;
    }
}
