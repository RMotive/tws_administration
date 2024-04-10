using Customer.Contracts.Models;

using Foundation.Contracts.Modelling.Bases;

using TWS_Security.Entities;

namespace Customer.Models;

public class SessionModel
    : BModel, IPrivateModel<ForeignSessionModel> {
    public Guid Token { get; private set; }
    public AccountEntity Account { get; private set; }
    public DateTime GenerationTimemark { get; private set; }
    public DateTime UpdateTimemark { get; private set; }
    public DateTime ExpirationTimemark { get; private set; }
    public TimeSpan ExpirationTimemarkThreshold { get; private set; }
    public List<int> Features { get; private set; }

    public SessionModel(Guid token, AccountEntity account, TimeSpan threshold, List<int> features) {
        Token = token;
        Account = account;
        GenerationTimemark = DateTime.UtcNow;
        UpdateTimemark = GenerationTimemark;
        ExpirationTimemark = GenerationTimemark.Add(threshold);
        ExpirationTimemarkThreshold = threshold;
        Features = features;
    }

    public ForeignSessionModel GeneratePublicDerivation()
    => new(Token, Features, Account.Wildcard, ExpirationTimemark);
}
