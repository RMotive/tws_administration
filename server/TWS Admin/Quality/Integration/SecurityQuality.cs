using System.Net;
using System.Net.Http.Json;
using Customer.Transactions.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Server;
using Xunit;

namespace Quality.Integration
{
    /// <summary>
    ///     Performs all the quality integration tests for SecurityController
    /// </summary>
    public class SecurityControllerQuality : IClassFixture<WebApplicationFactory<Program>>
    {
        /// <summary>
        ///     Represents the address to perform the login action
        /// </summary>
        const string performLoginAddress = "Security/PerformLogin";
        /// <summary>
        ///     Stores the instance of a WebApplicationFactory to perform testing operations in the actual exposed server.
        /// </summary>
        private readonly HttpClient _serverClient;
        public SecurityControllerQuality(WebApplicationFactory<Program> factory)
        {
            _serverClient = factory.CreateClient();
        }

        [Fact]
        public void PerformLoginFail()
        {
            PerformLoginRequest requestData = new PerformLoginRequest
            {
                Identity = "testiong12",
                Security = "test12",
            };
            HttpResponseMessage actionResult = _serverClient.PostAsJsonAsync(performLoginAddress, requestData).Result;
            // --> Checking if the response is sucess
            Assert.Equal(HttpStatusCode.OK, actionResult.StatusCode);
        }
    }
}

