﻿using System.Net.Http.Json;

using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Servers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Server.Middlewares;
using Server.Quality.Templates;

using Xunit;

using SCodes = System.Net.HttpStatusCode;

namespace Server.Quality.Tests.Middlewares;
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
                        jOptions.JsonSerializerOptions.WriteIndented = true;
                        jOptions.JsonSerializerOptions.IncludeFields = true;
                        jOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
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
                            throw new XServerConfiguration(ServerConfigurationFailureReasons.NotFound);
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

        GenericExposure? FailureResponse = await Response.Content.ReadFromJsonAsync<GenericExposure>();

        Assert.Equal(SCodes.InternalServerError, Response.StatusCode);
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

        GenericExposure? FailureResponse = await Response.Content.ReadFromJsonAsync<GenericExposure>();

        Assert.Equal(SCodes.BadRequest, Response.StatusCode);
        Assert.NotNull(FailureResponse);

    }
}
