using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using EnlaceFiscal.Core.Enums;
using EnlaceFiscal.Models;
using EnlaceFiscal.Models.Inputs;
using EnlaceFiscal.Models.Outputs;

namespace EnlaceFiscal;
public class EnlaceFiscal : IEnlaceFiscal {
    readonly string Mode;
    readonly Credentials Credentials;

    public EnlaceFiscal(Credentials Credentials, EnlaceFiscalModos Mode) {
        this.Credentials = Credentials;

        this.Mode = Mode.ToString().ToLower();
    }

    public async Task<OAckEnlaceFiscal<ProbarConexionOutput>> ProbarConexión() {
        const string URL = "https://api.enlacefiscal.com/v6/probarConexion";

        HttpClient httpClient = new();

        ProbarConexiónInput actionInput = new() { 
            Modo = Mode,
            RFC = Credentials.Usuario,
            Accion = "probarConexion",
        };
        OSolicitud<ProbarConexiónInput> input = new(actionInput);

        string auth = $"{Credentials.Usuario}:{Credentials.Token}";
        string encodedAuth = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));

        httpClient.DefaultRequestHeaders.Add("x-api-key", Credentials.Key);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);
        string body = JsonSerializer.Serialize(input);

        StringContent requestContent = new(body, Encoding.UTF8, "application/json");

        HttpResponseMessage requestResponse = await httpClient.PostAsync(URL, requestContent);
        Stream responseContent = requestResponse.Content.ReadAsStream();
        OAckEnlaceFiscal<ProbarConexionOutput> actionOutput = JsonSerializer.Deserialize<OAckEnlaceFiscal<ProbarConexionOutput>>(responseContent)
            ?? throw new JsonException("Unable to convert request response into a valid object");

        return actionOutput;
    }
}
