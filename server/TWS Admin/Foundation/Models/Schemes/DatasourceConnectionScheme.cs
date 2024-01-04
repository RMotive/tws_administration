using Foundation.Contracts.Modelling.Bases;

namespace Foundation.Models.Schemes;
public class DatasourceConnectionScheme 
    : BScheme<DatasourceConnectionModel> {
    public string Host { get; set; } = "";
    public string Database { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
    public bool Encrypted { get; set; } = false; 

    public override DatasourceConnectionModel GenerateModel() {
        return new DatasourceConnectionModel(Host, Database, User, Password, Encrypted);
    }
}
