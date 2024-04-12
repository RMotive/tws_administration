using Foundation.Contracts.Modelling.Bases;

namespace Foundation.Models.Schemes;
public class DatasourceConnectionScheme
    : BScheme<DatasourceConnectionScheme, DatasourceConnectionModel> {
    public string Host { get; set; } = "";
    public string Database { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
    public bool Encrypted { get; set; } = false;

    protected override DatasourceConnectionModel Generate() {
        return new DatasourceConnectionModel(Host, Database, User, Password, Encrypted);
    }
}
