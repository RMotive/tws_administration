using System.Net;
using System.Text.Json;

using CSMFoundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Helpers;

using Xunit;

namespace CSMFoundation.Servers.Quality.Bases;
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

    public BQ_ServerController(string Service, WebApplicationFactory<TEntry> Factory) {
        this.Service = Service;
        this.Host = new(Factory.CreateClient());
    }

    #region Protected Abstract Methods

    protected abstract Task<string> Authentication();

    #endregion

    #region Protected Methods 

    protected TFrame Framing<TFrame>(ServerGenericFrame Generic) {
        string desContent = JsonSerializer.Serialize(Generic);

        TFrame frame = JsonSerializer.Deserialize<TFrame>(desContent)!;
        return frame;
    }
    protected async Task<(HttpStatusCode, ServerGenericFrame)> Post<TRequest>(string Action, TRequest Request, bool Authenticate = false)
    => await Post<ServerGenericFrame, TRequest>(Action, Request, false, Authenticate);
    protected async Task<(HttpStatusCode, TResponse)> Post<TResponse, TRequest>(string Action, TRequest Request, bool Authenticate = false)
    => await Post<TResponse, TRequest>(Action, Request, false, Authenticate);
    protected async Task<(HttpStatusCode, TResponse)> XPost<TResponse, TRequest>(string Free, TRequest Request, bool Authenticate = false)
    => await Post<TResponse, TRequest>(Free, Request, true, Authenticate);

    #endregion

    #region Private Methods 

    private async Task<(HttpStatusCode, TResponse)> Post<TResponse, TRequest>(string Action, TRequest RequestBody, bool FreeAction, bool Authenticate = false, string Disposition = "Quality") {
        if (Authenticate) {
            string authToken = await Authentication();
            Host.Authenticate(authToken);
        }
        if (!FreeAction) {
            Action = $"{Service}/{Action}";
        }

        Host.Disposition(Disposition);
        return await Host.Post<TResponse, TRequest>(Action, RequestBody);
    }
    #endregion
}
