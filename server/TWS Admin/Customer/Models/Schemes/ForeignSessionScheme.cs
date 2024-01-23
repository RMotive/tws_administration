using Foundation.Contracts.Modelling.Interfaces;

namespace Customer.Models.Schemes;
public class ForeignSessionScheme 
    : IScheme<ForeignSessionModel> {
    public string Token { get; set; }
    public List<int> Features { get; set; }

    public ForeignSessionScheme() {
        Token = string.Empty;
        Features = [];
    }

    public ForeignSessionModel GenerateModel() {
        return new ForeignSessionModel(Token, Features);
    }
}
