using System.Net;

using CSM_Foundation.Server.Quality.Bases;
using CSM_Foundation.Server.Records;
using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;

using TWS_Business.Sets;

using TWS_Customer.Managers.Records;
using TWS_Customer.Services.Records;

using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = CSM_Foundation.Source.Models.Out.SetViewOut<TWS_Business.Sets.Situation>;

namespace Server.Quality.Suit.Controllers;
public class Q_SituationController : BQ_ServerController<Program> {
    private class Frame : SuccessFrame<View> { }


    public Q_SituationController(WebApplicationFactory<Program> hostFactory)
        : base("Situations", hostFactory) {
    }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, SuccessFrame<Session> Response) = await XPost<SuccessFrame<Session>, Credentials>("Security/Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        return Status != HttpStatusCode.OK ? throw new ArgumentNullException(nameof(Status)) : Response.Estela.Token.ToString();
    }

    [Fact]
    public async Task View() {
        (HttpStatusCode Status, ServerGenericFrame Response) = await Post("View", new SetViewOptions {
            Page = 1,
            Range = 10,
            Retroactive = false,
        }, true);

        Assert.Equal(HttpStatusCode.OK, Status);

        View Estela = Framing<SuccessFrame<View>>(Response).Estela;
        Assert.True(Estela.Sets.Length > 0);
        Assert.Equal(1, Estela.Page);
        Assert.True(Estela.Pages > 0);
    }

    [Fact]
    public async Task Create() {
        string testTag = Guid.NewGuid().ToString()[..3];
        string name = " name ctl" + testTag;
        string description = "desc ctl" + testTag;

        Situation mock = new() {
            Name = name,
            Description = description
        };

        (HttpStatusCode Status, ServerGenericFrame Response) = await Post("Create", mock, true);

        _ = Response.Estela.TryGetValue("Advise", out object? value);
        Assert.Null(value);
        Assert.Equal(HttpStatusCode.OK, Status);
    }
}