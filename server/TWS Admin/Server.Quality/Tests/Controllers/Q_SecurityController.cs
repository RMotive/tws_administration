using System.Net;

using Customer;
using Customer.Models.Schemes;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Quality.Helpers;
using Server.Quality.Private;
using Server.Templates.Exposures;

using Xunit;

namespace Server.Quality.Tests.Controllers;
/// <summary>
///     This quality test class ensures the quality and functionallities for all
///     the controllers managed by the Security Controller.
/// </summary>
public class Q_SecurityController
    : IClassFixture<WebApplicationFactory<Program>> {
}
