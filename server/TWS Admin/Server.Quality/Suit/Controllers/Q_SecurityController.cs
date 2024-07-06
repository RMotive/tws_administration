using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using CSMFoundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Bases;
using Server.Quality.Secrets;

using Xunit;

using PrivilegesFrame = Server.Middlewares.Frames.SuccessFrame<Customer.Managers.Records.Session>;

namespace Server.Quality.Controllers;

public class Q_SecurityController
    : BQ_CustomServerController {
    public Q_SecurityController(WebApplicationFactory<Program> hostFactory)
        : base("Security", hostFactory) { }

    [Fact]
    public async Task Authenticate() {
        await Authentication();
    }
}
