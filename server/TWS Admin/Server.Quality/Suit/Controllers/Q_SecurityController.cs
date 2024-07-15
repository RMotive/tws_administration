using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Bases;

using Xunit;

namespace Server.Quality.Suit.Controllers;

public class Q_SecurityController
    : BQ_CustomServerController {
    public Q_SecurityController(WebApplicationFactory<Program> hostFactory)
        : base("Security", hostFactory) { }

    [Fact]
    public async Task Authenticate() {
        _ = await Authentication();
    }
}
