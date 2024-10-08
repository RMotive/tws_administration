﻿using System.Net;

using Customer.Managers.Records;
using Customer.Services.Records;

using Foundation.Migrations.Records;
using Foundation.Server.Records;
using Foundation.Servers.Quality.Bases;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;
using Server.Quality.Bases;

using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = Foundation.Migrations.Records.MigrationView<TWS_Business.Sets.Maintenance>;

namespace Server.Quality.Controllers;
public class Q_MaintenanceController : BQ_CustomServerController {

    public Q_MaintenanceController(WebApplicationFactory<Program> hostFactory)
        : base("Maintenances", hostFactory) {
    }

    protected override async Task<string> Authentication() {
        (HttpStatusCode Status, SuccessFrame<Session> Response) = await XPost<SuccessFrame<Session>, Credentials>("Security/Authenticate", new Credentials {
            Identity = Account.Identity,
            Password = Account.Password,
        });

        if (Status != HttpStatusCode.OK)
            throw new ArgumentNullException(nameof(Status));
        return Response.Estela.Token.ToString();
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
}

