using Foundation.Contracts.Modelling.Interfaces;

namespace Customer.Models.Schemes;
public class ForeignSessionScheme
    : IScheme<ForeignSessionModel> {
    public Guid Token { get; set; }
    public bool Wildcard { get; set; } = false;
    public List<int> Features { get; set; } = [];

    public ForeignSessionScheme() { }
    public ForeignSessionModel GenerateModel() {
        return new ForeignSessionModel(Token, Features, Wildcard);
    }
}
