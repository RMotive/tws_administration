

using CSMFoundation.Server.Records;
using Microsoft.AspNetCore.Mvc.Testing;
using Server.Quality.Bases;
using System.Net;
using TWS_Security.Sets;
using Xunit;

namespace Server.Quality.Controllers;
public class Q_ContactsController
    : BQ_CustomServerController {

    public Q_ContactsController(WebApplicationFactory<Program> hostFactory)
        : base("Contacts", hostFactory) { 

    }

    [Fact]
    public async Task Create() {
        {
            Contact[] mocks = [];
            for (int i = 0; i < 3; i++) {
                string uniqueToken = Guid.NewGuid().ToString();

                mocks = [
                    ..mocks,
                    new Contact {
                        Name = $"{i}_{uniqueToken[..20]}",
                        Lastname = $"{i}_{uniqueToken[..20]}",
                        Email = $"{i}_{uniqueToken[..20]}",
                        Phone = $"{i}_{uniqueToken[..14]}"
                    },
                ];
            }

            (HttpStatusCode Status, ServerGenericFrame Frame) fact = await Post("Create", mocks, true);

            Assert.Equal(HttpStatusCode.OK, fact.Status);
        }
    }
}
