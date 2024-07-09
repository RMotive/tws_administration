using System.Net;
using CSMFoundation.Core.Utils;
using CSMFoundation.Server.Records;
using CSMFoundation.Source.Models.Out;
using Microsoft.AspNetCore.Mvc.Testing;

using Server.Middlewares.Frames;
using Server.Quality.Bases;

using TWS_Security.Sets;

using Xunit;

using View = CSMFoundation.Source.Models.Out.SetViewOut<TWS_Security.Sets.Solution>;

namespace Server.Quality.Controllers;


public class Q_SolutionsController
    : BQ_CustomServerController {

    public Q_SolutionsController(WebApplicationFactory<Program> hostFactory)
        : base("Solutions", hostFactory) {
    }

    [Fact]
    public async Task View() {
        (HttpStatusCode Status, ServerGenericFrame Response) fact = await Post("View", new SetView {
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

    [Fact]
    public async Task Update() {
        #region First (Correctly creates when doesn't exist)
        {
            (HttpStatusCode Status, ServerGenericFrame Respone) creationFact = await Post("Update", new Solution {
                Id = 0,
                Name = RandomUtils.String(10),
                Sign = RandomUtils.String(5),
                Description = RandomUtils.String(10),
            }, true);

            Assert.Equal(HttpStatusCode.OK, creationFact.Status);
            RecordUpdateOut<Solution> creationResult = Framing<SuccessFrame<RecordUpdateOut<Solution>>>(creationFact.Respone).Estela;

            Assert.Null(creationResult.Previous);

            Solution updated = creationResult.Updated;
            Assert.True(updated.Id > 0);
        }
        #endregion

        #region Second (Updates an exist record)
        {
            Solution mock = new() {
                Name = RandomUtils.String(10),
                Sign = RandomUtils.String(5),
                Description = RandomUtils.String(10),
            };
            (HttpStatusCode Status, ServerGenericFrame Response) creationResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, creationResponse.Status);

            RecordUpdateOut<Solution> creationResult = Framing<SuccessFrame<RecordUpdateOut<Solution>>>(creationResponse.Response).Estela;
            Assert.Null(creationResult.Previous);

            Solution creationRecord = creationResult.Updated;
            Assert.Multiple([
                () => Assert.True(creationRecord.Id > 0),
                () => Assert.Equal(mock.Name, creationRecord.Name),
                () => Assert.Equal(mock.Sign, creationRecord.Sign),
                () => Assert.Equal(mock.Description, creationRecord.Description),
            ]);

            mock.Id = creationRecord.Id;
            mock.Name = RandomUtils.String(10);
            (HttpStatusCode Status, ServerGenericFrame Response) updateResponse = await Post("Update", mock, true);

            Assert.Equal(HttpStatusCode.OK, updateResponse.Status);
            RecordUpdateOut<Solution> updateResult = Framing<SuccessFrame<RecordUpdateOut<Solution>>>(updateResponse.Response).Estela;

            Assert.NotNull(updateResult.Previous);

            Solution updateRecord = updateResult.Updated;
            Assert.Multiple([
                () => Assert.Equal(creationRecord.Id, updateRecord.Id),
                () => Assert.Equal(creationRecord.Sign, updateRecord.Sign),
                () => Assert.Equal(creationRecord.Description, updateRecord.Description),
                () => Assert.NotEqual(creationRecord.Name, updateRecord.Name),
            ]);
        }
        #endregion
    }
}