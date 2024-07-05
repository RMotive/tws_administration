using System.Net;

using Foundation.Migrations.Records;
using Foundation.Server.Records;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;
using Server.Quality.Bases;

using TWS_Security.Sets;

using Xunit;

using View = Foundation.Migrations.Records.MigrationView<TWS_Security.Sets.Solution>;

namespace Server.Quality.Controllers;


public class Q_SolutionsController
    : BQ_CustomServerController {

    public Q_SolutionsController(WebApplicationFactory<Program> hostFactory)
        : base("Solutions", hostFactory) {
    }

    [Fact]
    public async Task View() {
        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("View", new MigrationViewOptions {
            Page = 1,
            Range = 10,
            Retroactive = false,
        }, true);

        Assert.Equal(HttpStatusCode.OK, fact.Status);

        View Estela = Framing<SuccessFrame<View>>(fact.Response).Estela;
        Assert.True(Estela.Sets.Length > 0);
        Assert.Equal(1, Estela.Page);
        Assert.True(Estela.Pages > 0);
    }

    [Fact]
    public async Task Create() {
        #region First (Correctly creates 3 Solutions)
        {
            Solution[] mocks = [];
            for (int i = 0; i < 3; i++) {
                string uniqueToken = Guid.NewGuid().ToString();

                mocks = [
                    ..mocks,
                    new Solution {
                        Name = $"{i}_{uniqueToken[..10]}",
                        Sign = $"{i}{uniqueToken[..4]}",
                    },
                ];
            }

            (HttpStatusCode Status, ServerGenericFrame Frame) fact = await Post("Create", mocks, true);

            Assert.Equal(HttpStatusCode.OK, fact.Status);
        }
        #endregion
    }

}
