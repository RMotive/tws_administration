using Foundation.Contracts.Modelling.Bases;

namespace Foundation.Models;
/// <summary>
///     
/// </summary>
public class DatasourceConnectionModel
    : BModel {
    public string Host { get; private set; }
    public string Database { get; private set; }
    public string User { get; private set; }
    public string Password { get; private set; }
    public bool Encrypted { get; private set; }

    public DatasourceConnectionModel(string host, string database, string user, string password, bool encrypted) {
        Host = host;
        Database = database;
        User = user;
        Password = password;
        Encrypted = encrypted;
    }
}
