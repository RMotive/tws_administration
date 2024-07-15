using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Http;

namespace CSM_Foundation.Server.Quality.Managers;
public class QM_ServerHost(HttpClient host) {
    private const string AUTH_TOKEN = "CSMAuth";
    private const string DISPOSITION_TOKEN = "CSMDisposition";
    private readonly HttpClient Host = host;

    public async Task<(HttpStatusCode, TResponse)> Post<TResponse, TRequest>(string Location, TRequest Request) {
        HttpResponseMessage Response = await Host.PostAsJsonAsync(Location, Request);
        HttpStatusCode resolutionCode = Response.StatusCode;
        TResponse resolution = await Response.Content.ReadFromJsonAsync<TResponse>()
            ?? throw new Exception("Nullified deserealization");

        Restore();
        return (resolutionCode, resolution);
    }

    public void Dispose() {
        Host.Dispose();
    }
    private void Restore() {
        Host.DefaultRequestHeaders.Clear();
    }

    public void Authenticate(string Token) {
        Host.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTH_TOKEN, Token);
    }

    public void Disposition(string Disposition) {
        Host.DefaultRequestHeaders.Add(DISPOSITION_TOKEN, Disposition);
    }
}
