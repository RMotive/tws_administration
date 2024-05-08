using System.Net;
using System.Text.Json;

using Foundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

using Server.Quality.Helpers;

using Xunit;

namespace Foundation.Servers.Quality.Bases;
/// <summary>
///     Defines base behaviors for quality operations to 
///     <see cref="BQ_Controller"/> implementations.
///     
///     <br></br>
///     <br> A Controller is the server exposition for endpoints and another services. </br>
/// </summary>
/// <typeparam name="TEntry">
///     Entry class that starts your server project.
/// </typeparam>
public abstract class BQ_ServerController<TEntry>
    : IClassFixture<WebApplicationFactory<TEntry>>
    where TEntry : class {

    readonly string Service;
    readonly QM_ServerHost Host;

    public BQ_ServerController(string Service, WebApplicationFactory<TEntry> hostFactory) {
        this.Service = Service;
        this.Host = new(hostFactory.CreateClient());
    }


    protected abstract Task<string> Authentication();
    protected void Restore<TSource, TSet>(TSet Set) 
        where TSource : DbContext, new() {
        
        TSource source = new();
    }
    protected TFrame Framing<TFrame>(ServerGenericFrame Generic) {
        string desContent = JsonSerializer.Serialize(Generic);

        TFrame frame = JsonSerializer.Deserialize<TFrame>(desContent)!;
        return frame;
    }
    protected async Task<(HttpStatusCode, ServerGenericFrame)> Post<TRequest>(string Action, TRequest Request, bool Authenticate = false) {
        if (Authenticate) {
            Host.Authenticate(await Authentication());
        }
        return await Host.Post<ServerGenericFrame, TRequest>($"{Service}/{Action}", Request);
    }
    protected async Task<(HttpStatusCode, TPayload)> Post<TPayload, TRequest>(string Action, TRequest Request, bool Authenticate = false) {
        if (Authenticate) {
            Host.Authenticate(await Authentication());
        }
        return await Host.Post<TPayload, TRequest>($"{Service}/{Action}", Request);
    }
    protected async Task<(HttpStatusCode, TPayload)> XPost<TPayload, TRequest>(string Free, TRequest Request, bool Authenticate = false) {
        if (Authenticate) {
            Host.Authenticate(await Authentication());
        }
        return await Host.Post<TPayload, TRequest>(Free, Request);
    }
}
