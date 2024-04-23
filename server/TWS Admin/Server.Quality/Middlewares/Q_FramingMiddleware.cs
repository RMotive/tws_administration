using System.Net;
using System.Net.Http.Json;

using Foundation.Server.Records;
using Foundation.Servers.Quality.Bases;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Server.Middlewares;
using Server.Quality.Middlewares.Resources.Exceptions;

using Xunit;

namespace Server.Quality.Middlewares;
/// <summary>
///     Test class context.
///     This test class tests the quality of the <seealso cref="FramingMiddleware"/> implementation inside the server.
/// </summary>
public class Q_FramingMiddleware
    : BQ_ServerMiddleware {
    /// <summary>
    ///     Reference for the endpoint configured to test the catching of unspeficied exceptions.
    /// </summary>
    const string SYSTEM_EXCEPTION_EP = "system";
    /// <summary>
    ///     Reference for the endpoint configured to test the catchiing of foundation specified exceptions.
    /// </summary>
    const string FOUNDATION_EXCEPTION_EP = "foundation";

    /// <summary>
    ///     Quality test constructor for this context.
    ///     
    ///     Here we configure the test server to limit the context to the test functions required.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="XServerConfiguration"></exception>
    public Q_FramingMiddleware() { }

    protected override IHostBuilder Configuration() {
        return new HostBuilder()
            .ConfigureWebHost(webBuilder => {
                webBuilder.UseTestServer();
                webBuilder.ConfigureServices(services => {
                    services.AddControllers()
                    .AddJsonOptions(jOptions => {
                        jOptions.JsonSerializerOptions.WriteIndented = true;
                        jOptions.JsonSerializerOptions.IncludeFields = true;
                        jOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
                    services.AddRouting();
                    services.AddSingleton<AnalyticsMiddleware>();
                    services.AddSingleton<AdvisorMiddleware>();
                    services.AddSingleton<FramingMiddleware>();
                });
                webBuilder.Configure(app => {
                    app.UseRouting();
                    app.UseMiddleware<AnalyticsMiddleware>();
                    app.UseMiddleware<AdvisorMiddleware>();
                    app.UseMiddleware<FramingMiddleware>();
                    app.UseEndpoints(endPoints => {

                        endPoints.MapGet(SYSTEM_EXCEPTION_EP, () => {
                            throw new ArgumentException();
                        });
                        endPoints.MapGet(FOUNDATION_EXCEPTION_EP, () => {
                            throw new XQ_Exception();
                        });
                    });
                });
            });
    }

    [Fact]
    public async void SystemException() {
        using HttpClient Server = (await Host.StartAsync()).GetTestClient();

        HttpResponseMessage Response = await Server.GetAsync(SYSTEM_EXCEPTION_EP);

        string value = await Response.Content.ReadAsStringAsync();

        ServerGenericFrame? fact = await Response.Content.ReadFromJsonAsync<ServerGenericFrame>();

        Assert.Equal(HttpStatusCode.InternalServerError, Response.StatusCode);
        Assert.NotNull(fact);
        Assert.True(fact.Estela.ContainsKey("System"));

        string expectedExcep = typeof(ArgumentException).ToString();
        string actualExcep = fact.Estela["System"].ToString()?.Split('|')[0] ?? "";

        Assert.Equal(expectedExcep, actualExcep);
    }
    [Fact]
    public async void FoundationException() {
        using HttpClient Server = (await Host.StartAsync()).GetTestClient();

        HttpResponseMessage Response = await Server.GetAsync(FOUNDATION_EXCEPTION_EP);

        ServerGenericFrame? fact = await Response.Content.ReadFromJsonAsync<ServerGenericFrame>();

        Assert.Equal(HttpStatusCode.BadRequest, Response.StatusCode);
        Assert.NotNull(fact);
        Assert.True(fact.Estela.ContainsKey("System"));
        string expectedExcep = "N/A";
        string actualExcep = fact.Estela["System"].ToString()?.Split('|')[0] ?? "";

        Assert.Equal(expectedExcep, actualExcep);
    }
}
