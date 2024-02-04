using System.Net;

using Customer;
using Customer.Models.Schemes;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Helpers;
using Server.Quality.Private;

using Xunit;

namespace Server.Quality.Tests.Controllers;
/// <summary>
///     This quality test class ensures the quality and functionallities for all
///     the controllers managed by the Security Controller.
/// </summary>
public class Q_SecurityController 
    : IClassFixture<WebApplicationFactory<Program>> {
    const string Controller = "security/";

    /// <summary>
    ///     Host controllers manager.
    /// </summary>
    private readonly QualityHost HostManager;

    private readonly AccountIdentityScheme WrongScheme;
    private readonly AccountIdentityScheme CorrectScheme;

    /// <summary>
    ///     Configurations pre-testing.
    /// </summary>
    /// <param name="hostFactory"></param>
    public Q_SecurityController(WebApplicationFactory<Program> hostFactory) { 
        HostManager = new QualityHost(hostFactory.CreateClient());
        CorrectScheme = AccountIdentityPrivates.CorrectScheme;
        WrongScheme = new() {
            Identity = "something wrong",
            Password = [.."somethingwrong"u8],
        };
    }

    [Fact]
    public async void InitSession() {
        const string Service = $"{Controller}initSession";

        (HttpStatusCode ResponseCode, ForeignSessionScheme? ResponseBody) FirstFact = await HostManager.Post<AccountIdentityScheme, ForeignSessionScheme>(Service, CorrectScheme);

        Assert.Equal(HttpStatusCode.OK, FirstFact.ResponseCode);
        Assert.NotNull(FirstFact.ResponseBody);
    }
}
