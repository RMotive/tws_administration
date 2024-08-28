
using System.Net;

using CSM_Foundation.Core.Utils;
using CSM_Foundation.Server.Quality.Bases;
using CSM_Foundation.Server.Records;
using CSM_Foundation.Source.Models.Options;
using CSM_Foundation.Source.Models.Out;

using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;

using TWS_Business.Sets;

using TWS_Customer.Managers.Records;
using TWS_Customer.Services.Records;

using TWS_Security.Sets;

using Xunit;

using Account = Server.Quality.Secrets.Account;
using View = CSM_Foundation.Source.Models.Out.SetViewOut<TWS_Business.Sets.YardLog>;


namespace Server.Quality.Suit.Controllers;
public class Q_YardLogsController : BQ_ServerController<Program> {
    private class Frame : SuccessFrame<View> { }


    public Q_YardLogsController(WebApplicationFactory<Program> hostFactory)
        : base("YardLogs", hostFactory) {
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
        DateOnly date = new(2024, 12, 12);
        List<YardLog> mockList = new();
        string testTag = Guid.NewGuid().ToString()[..2];

        for (int i = 0; i < 3; i++) {
            string iterationTag = testTag + i;

            YardLog mock = new() {
                Entry = true,
                Truck = i,
                Trailer = i,
                LoadType = i,
                Guard = i,
                Gname = "Enrique" + iterationTag,
                Section = i,
                FromTo = "Cocacola florido " + iterationTag,
                Damage = false,
                TTPicture = "Foto " + iterationTag,
                Driver = i,
            };
            mockList.Add(mock);
        }

        (HttpStatusCode Status, ServerGenericFrame response) = await Post("Create", mockList, true);
        Assert.Equal(HttpStatusCode.OK, Status);

    }

    [Fact]
    public async Task Update() {
        string tag = RandomUtils.String(3);
        #region First (Correctly creates when doesn't exist)
        {
            YardLog mock = new() {
                Entry = true,
                Truck = 1,
                Trailer = 1,
                LoadType = 1,
                Guard = 1,
                Gname = "Enrique" + tag,
                Section = 1,
                FromTo = "Cocacola florido " + tag,
                Damage = false,
                TTPicture = "Foto " + tag,
                Driver = 1,
            };

            (HttpStatusCode Status, ServerGenericFrame Respone) = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, Status);
            RecordUpdateOut<Solution> creationResult = Framing<SuccessFrame<RecordUpdateOut<Solution>>>(Respone).Estela;

            Assert.Null(creationResult.Previous);

            Solution updated = creationResult.Updated;
            Assert.True(updated.Id > 0);
        }
        #endregion

        #region Second (Updates an exist record)
        {
            tag = "UPT " + RandomUtils.String(3) ;
            Section section = new() {
                Status = 1,
                Yard = 1,
                Name = RandomUtils.String(10),
                Capacity = 30,
                Ocupancy = 1
            };
            YardLog mock = new() {
                Entry = true,
                Truck = 1,
                Trailer = 1,
                LoadType = 1,
                Guard = 1,
                Gname = "Enrique" + tag,
                SectionNavigation = section,
                FromTo = "Cocacola florido " + tag,
                Damage = false,
                TTPicture = "Foto " + tag,
                Driver = 1,
            };
            (HttpStatusCode Status, ServerGenericFrame Response) = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, Status);

            RecordUpdateOut<YardLog> creationResult = Framing<SuccessFrame<RecordUpdateOut<YardLog>>>(Response).Estela;
            Assert.Null(creationResult.Previous);

            YardLog creationRecord = creationResult.Updated;
            Assert.Multiple([
                () => Assert.True(creationRecord.Id > 0),
                () => Assert.Equal(mock.Gname, creationRecord.Gname),
                () => Assert.Equal(mock.FromTo, creationRecord.FromTo),
                () => Assert.Equal(mock.TTPicture, creationRecord.TTPicture),
                () => Assert.Equal(mock.SectionNavigation.Name, creationRecord.SectionNavigation!.Name),
            ]);

            mock.Id = creationRecord.Id;
            mock.Timestamp = creationRecord.Timestamp;
            mock.Gname = RandomUtils.String(10);
            mock.Section = creationRecord.SectionNavigation!.Id;
            mock.SectionNavigation.Id = creationRecord.SectionNavigation!.Id;
            mock.SectionNavigation.Name = "UPT" + RandomUtils.String(10);
            (HttpStatusCode Status, ServerGenericFrame Response) updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            RecordUpdateOut<YardLog> updateResult = Framing<SuccessFrame<RecordUpdateOut<YardLog>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            YardLog updateRecord = updateResult.Updated;
            Assert.Multiple([
                () => Assert.Equal(creationRecord.Id, updateRecord.Id),
                () => Assert.Equal(creationRecord.FromTo, updateRecord.FromTo),
                () => Assert.Equal(creationRecord.TTPicture, updateRecord.TTPicture),
                () => Assert.NotEqual(creationRecord.Gname, updateRecord.Gname),
                () => Assert.NotEqual(creationRecord.SectionNavigation!.Name, updateRecord.SectionNavigation!.Name),
            ]);
        }
        #endregion
    }
}