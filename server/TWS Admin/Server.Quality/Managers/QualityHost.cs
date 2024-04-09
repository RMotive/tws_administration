using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Http;

namespace Server.Quality.Helpers;
public class QualityHost {
    readonly HttpClient Host;
    public QualityHost(HttpClient host) {
        Host = host;
    }

    public async Task<(HttpStatusCode, TExposure?)> Post<TScheme, TExposure>(string endpoint, TScheme Object) {
        HttpResponseMessage Response = await Host.PostAsJsonAsync(endpoint, Object);
        HttpStatusCode ResponseCode = Response.StatusCode;
        TExposure? Exposure = await Response.Content.ReadFromJsonAsync<TExposure>();
        return (ResponseCode, Exposure);
    }
}
