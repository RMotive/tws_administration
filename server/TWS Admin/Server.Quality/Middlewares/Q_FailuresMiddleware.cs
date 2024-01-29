using System.Net.Http.Json;

using Foundation.Contracts.Exceptions;
using Foundation.Exceptions.Servers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Server.Middlewares;
using Server.Quality.Schemes;

using Xunit;

namespace Server.Quality.Middlewares;
public class Q_FailuresMiddleware {
    const string UNCGHT_EXCEPTION_ENDPOINT = "uncaugth";
    const string BASE_EXCEPTION_ENDPOINT = "base";

    readonly IHostBuilder Host;

    public Q_FailuresMiddleware() {
        Host = new HostBuilder()
            .ConfigureWebHost(webBuilder => {
                webBuilder.UseTestServer();
                webBuilder.ConfigureServices(services => {
                    services.AddControllers()
                    .AddJsonOptions(jOptions => {
                        jOptions.JsonSerializerOptions.IncludeFields = true;
                    });
                    services.AddRouting();
                    services.AddSingleton<FailuresMiddleware>();
                });
                webBuilder.Configure(app => {  
                    app.UseRouting();
                    app.UseMiddleware<FailuresMiddleware>();
                    app.UseEndpoints(endPoints => {
                        
                        endPoints.MapGet(UNCGHT_EXCEPTION_ENDPOINT, () => {
                            throw new ArgumentException("TESTED ARGUMENTED EXCEPTION");
                        });
                        endPoints.MapGet(BASE_EXCEPTION_ENDPOINT, () => {
                            throw new XServerContext(XServerContext.Reason.NotFound);
                        });
                    });
                });
            });
    }
    /// <summary>
    ///     Tests if the <seealso cref="FailuresMiddleware"/> can successfully catch and parse uncaught system exceptions 
    ///     and return a cosmetic exception error.
    /// </summary>
    [Fact]
    public async void UncaugthMiddleware() {
        using HttpClient Server = (await Host.StartAsync()).GetTestClient();

        HttpResponseMessage Response = await Server.GetAsync(UNCGHT_EXCEPTION_ENDPOINT);

        FailureTemplateScheme<XServerFailure>? FailureResponse = await Response.Content.ReadFromJsonAsync<FailureTemplateScheme<XServerFailure>>();

        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, Response.StatusCode);
        Assert.NotNull(FailureResponse);
    }
    /// <summary>
    ///     Tests if the <seealso cref="FailuresMiddleware"/> can successfully catch and parse caugth exceptions converted to BException
    ///     and return the cosmetic way.
    /// </summary>
    [Fact]
    public async void BaseMiddleware() {
        using HttpClient Server = (await Host.StartAsync()).GetTestClient();

        HttpResponseMessage Response = await Server.GetAsync(BASE_EXCEPTION_ENDPOINT);

        FailureTemplateScheme<BException>? FailureResponse = await Response.Content.ReadFromJsonAsync<FailureTemplateScheme<BException>>();

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, Response.StatusCode);
        Assert.NotNull(FailureResponse);
        Assert.IsNotType<FailureTemplateScheme<XServerFailure>>(FailureResponse);
        Assert.IsAssignableFrom<BException>(FailureResponse);
    }
}
