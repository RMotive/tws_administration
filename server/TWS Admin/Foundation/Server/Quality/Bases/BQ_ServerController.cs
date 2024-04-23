using System.Net;
using System.Text.Json;

using Foundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;

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
    protected TFrame Framing<TFrame>(ServerGenericFrame Generic) {
        string desContent = JsonSerializer.Serialize(Generic);

        TFrame frame = JsonSerializer.Deserialize<TFrame>(desContent)!;
        return frame;
    }
    protected async Task<(HttpStatusCode, ServerGenericFrame)> Post(string Action, object Request, bool Authenticate = false) {
        if (Authenticate) {
            Host.Authenticate(await Authentication());
        }
        return await Host.Post<ServerGenericFrame>($"{Service}/{Action}", Request);
    }
    protected async Task<(HttpStatusCode, TPayload)> Post<TPayload>(string Action, object Request, bool Authenticate = false) {
        if (Authenticate) {
            Host.Authenticate(await Authentication());
        }
        return await Host.Post<TPayload>($"{Service}/{Action}", Request);
    }
    protected async Task<(HttpStatusCode, TPayload)> XPost<TPayload>(string Free, object Request, bool Authenticate = false) {
        if (Authenticate) {
            Host.Authenticate(await Authentication());
        }
        return await Host.Post<TPayload>(Free, Request);
    }
}
