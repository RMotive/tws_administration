using Microsoft.AspNetCore.Mvc.Testing;

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

}
