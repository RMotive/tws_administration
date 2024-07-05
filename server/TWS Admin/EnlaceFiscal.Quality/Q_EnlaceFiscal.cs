using System.Text.Json;

using EnlaceFiscal.Core.Enums;
using EnlaceFiscal.Models;
using EnlaceFiscal.Models.Outputs;

namespace EnlaceFiscal.Quality;
public class Q_EnlaceFiscal {
    readonly EnlaceFiscal enlaceFiscal;

    public Q_EnlaceFiscal() {
        Credentials credentials = GetCredentials();
        enlaceFiscal = new EnlaceFiscal(credentials, EnlaceFiscalModos.Debug);
    }

    static Credentials GetCredentials() {
        string directory = Directory.GetCurrentDirectory();
        string[] innerDirectories = Directory.GetDirectories(directory);
        string propertiesDirectory = innerDirectories
            .Where(i => i.Contains("Properties"))
            .FirstOrDefault()
            ?? throw new DirectoryNotFoundException("[Properties] directory not found");

        string[] propertiesFiles = Directory.GetFiles(propertiesDirectory);
        string credentialsFile = propertiesFiles
            .Where(i => i.Contains("credentials.json"))
            .FirstOrDefault()
            ?? throw new FileNotFoundException("[credentials.json] file not found");

        string file = File.ReadAllText(credentialsFile);
        Credentials credentials = JsonSerializer.Deserialize<Credentials>(file)
            ?? throw new Exception("Unable to convert file to an object");

        return credentials;
    }

    [Fact]
    public async Task Q_ProbarConexión() {
        OAckEnlaceFiscal<ProbarConexionOutput> fact = await enlaceFiscal.ProbarConexión();

        Assert.Equal("aceptado", fact.AckEnlaceFiscal.EstatusDocumento);
    }
}
