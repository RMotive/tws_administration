using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using Azure.Core;

using Microsoft.AspNetCore.Http;

namespace Server.Quality.Helpers;
public class QM_ServerHost {
    const string AUTH_TOKEN = "CSMAuth";

    readonly HttpClient Host;
    public QM_ServerHost(HttpClient host) {
        Host = host;
    }

    public async Task<(HttpStatusCode, TResponse)> Post<TResponse>(string Location, object Request) {        
        HttpResponseMessage Response = await Host.PostAsJsonAsync(Location, Request);
        HttpStatusCode resolutionCode = Response.StatusCode;
        TResponse resolution = await Response.Content.ReadFromJsonAsync<TResponse>()
            ?? throw new Exception("Nullified deserealization");
        return (resolutionCode, resolution);
    }

    public void Authenticate(string Token) {
        Host.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTH_TOKEN, Token);
    }
}
