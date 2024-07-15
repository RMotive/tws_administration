using System.Net;
using System.Net.Http.Json;

using CSM_Foundation.Server.Bases;
using CSM_Foundation.Server.Quality.Bases;
using CSM_Foundation.Server.Records;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Server.Middlewares;
using Server.Quality.Suit.Middlewares.Resources.Exceptions;

using Xunit;

namespace Server.Quality.Suit.Middlewares;
/// <summary>
///     Test class context.
///     This test class tests the quality of the <seealso cref="FramingMiddleware"/> implementation inside the server.
/// </summary>
public class Q_FramingMiddleware
    : BQ_ServerMiddleware {
    private const string SYSTEM_PROPERTY_REFERENCE = nameof(BServerTransactionException<Enum>.System);

    /// <summary>
    ///     Reference for the endpoint configured to test the catching of unspeficied exceptions.
    /// </summary>
    private const string SYSTEM_EXCEPTION_EP = "system";

    /// <summary>
    ///     Reference for the endpoint configured to test the catchiing of foundation specified exceptions.
    /// </summary>
    private const string FOUNDATION_EXCEPTION_EP = "foundation";

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
                _ = webBuilder.UseTestServer();
                _ = webBuilder.ConfigureServices(services => {
                    _ = services.AddControllers()
                    .AddJsonOptions(jOptions => {
                        jOptions.JsonSerializerOptions.WriteIndented = true;
                        jOptions.JsonSerializerOptions.IncludeFields = true;
                        jOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
                    _ = services.AddRouting();
                    _ = services.AddSingleton<AnalyticsMiddleware>();
                    _ = services.AddSingleton<AdvisorMiddleware>();
                    _ = services.AddSingleton<FramingMiddleware>();
                });
                _ = webBuilder.Configure(app => {
                    _ = app.UseRouting();
                    _ = app.UseMiddleware<AnalyticsMiddleware>();
                    _ = app.UseMiddleware<AdvisorMiddleware>();
                    _ = app.UseMiddleware<FramingMiddleware>();
                    _ = app.UseEndpoints(endPoints => {

                        _ = endPoints.MapGet(SYSTEM_EXCEPTION_EP, () => {
                            throw new ArgumentException();
                        });
                        _ = endPoints.MapGet(FOUNDATION_EXCEPTION_EP, () => {
                            throw new XQ_Exception();
                        });
                    });
                });
            });
    }

    [Fact]
    public async Task SystemException() {
        using HttpClient Server = (await Host.StartAsync()).GetTestClient();

        HttpResponseMessage Response = await Server.GetAsync(SYSTEM_EXCEPTION_EP);

        string value = await Response.Content.ReadAsStringAsync();

        ServerGenericFrame? fact = await Response.Content.ReadFromJsonAsync<ServerGenericFrame>();

        Assert.Equal(HttpStatusCode.InternalServerError, Response.StatusCode);
        Assert.NotNull(fact);
        Assert.True(fact.Estela.ContainsKey(SYSTEM_PROPERTY_REFERENCE));

        string expectedExcep = typeof(ArgumentException).ToString();
        string actualExcep = fact.Estela[SYSTEM_PROPERTY_REFERENCE].ToString()?.Split('|')[0] ?? "";

        Assert.Equal(expectedExcep, actualExcep);
    }
    [Fact]
    public async Task FoundationException() {
        using HttpClient Server = (await Host.StartAsync()).GetTestClient();

        HttpResponseMessage Response = await Server.GetAsync(FOUNDATION_EXCEPTION_EP);

        ServerGenericFrame? fact = await Response.Content.ReadFromJsonAsync<ServerGenericFrame>();

        Assert.Equal(HttpStatusCode.BadRequest, Response.StatusCode);
        Assert.NotNull(fact);
        Assert.True(fact.Estela.ContainsKey(SYSTEM_PROPERTY_REFERENCE));
        string expectedExcep = "N/A";
        string actualExcep = fact.Estela[SYSTEM_PROPERTY_REFERENCE].ToString()?.Split('|')[0] ?? "";

        Assert.Equal(expectedExcep, actualExcep);
    }
}
