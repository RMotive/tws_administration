using System.Text.Json;

using Customer.Services;
using Customer.Services.Interfaces;

using Foundation.Advising.Interfaces;
using Foundation.Advising.Managers;
using Foundation.Server.Utils;
using Foundation.Shared.Exceptions;
using Foundation.Utils;

using Microsoft.AspNetCore.Server.Kestrel.Core;

using Server.Middlewares;
using Server.Models;

namespace Server;

public class Program {
    const string SETTINGS_LOCATION = "\\Properties\\server_properties.json";
    const string CORS_BLOCK_MESSAGE = "Request blocked by cors, is not part of allowed hosts";


    private static Settings? SettingsStore { get; set; }
    public static Settings Settings { get { return SettingsStore ??= RetrieveSettings(); } }
    static private Settings RetrieveSettings() {
        string ws = Directory.GetCurrentDirectory();
        string sl = FileUtils.FormatLocation(SETTINGS_LOCATION);
        Dictionary<string, dynamic> tempModel = FileUtils.Deserealize<Dictionary<string, dynamic>>($"{ws}{sl}");
        AdvisorManager.Note("Retrieving server settings", new Dictionary<string, dynamic> {
            {"Workspace", ws },
            {"Settings", sl },
        });
        string host = ServerUtils.GetHost();
        string[] listeners = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")?.Split(";") ?? [];
        tempModel.Add("Host", host);
        tempModel.Add("Listeners", listeners);

        return JsonSerializer.Deserialize<Settings>(JsonSerializer.Serialize(tempModel)) ?? throw new Exception();
    }
    public static void Main(string[] args) {
        AdvisorManager.Announce("Running engines (⌐■_■)");
        try {
            Settings s = Settings;
            AdvisorManager.Success("Server settings retrieved", s);

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            // Add services and overriding options to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IncludeFields = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            builder.Services.Configure<KestrelServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });
            builder.Services.AddCors(setup => {
                setup.AddDefaultPolicy(builder => {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.SetIsOriginAllowed(origin => {
                        string[] CorsPolicies = [];
                        Uri parsedUrl = new(origin);
                        bool isCorsAllowed = CorsPolicies.Contains(parsedUrl.Host);
                        AdvisorManager.Warning(CORS_BLOCK_MESSAGE, new() {
                            {nameof(isCorsAllowed), isCorsAllowed},
                            {nameof(parsedUrl), parsedUrl}
                        });
                        return isCorsAllowed;
                    });
                });
            });
            // --> Adding customer services
            {
                builder.Services.AddSingleton<ISolutionsService>(new SolutionsService(new()));
                builder.Services.AddSingleton<ISecurityService>(new SecurityService(new()));
            }
            // --> Adding middleware services
            {
                builder.Services.AddSingleton(new AnalyticsMiddleware());
                builder.Services.AddSingleton(new FramingMiddleware());
                builder.Services.AddSingleton(new AdvisorMiddleware());
            }
            WebApplication app = builder.Build();
            app.UseCors(action => {
                action.AllowAnyMethod();
                action.AllowAnyHeader();
                action.AllowAnyOrigin();
            });
            app.MapControllers();
            // --> Injecting middlewares to server
            {
                app.UseMiddleware<AnalyticsMiddleware>();
                app.UseMiddleware<AdvisorMiddleware>();
                app.UseMiddleware<FramingMiddleware>();
            }
            app.Run();
        } catch (Exception X) when (X is IAdvisingException AX) {
            AdvisorManager.Exception(AX);
            throw;
        } catch (Exception X) {
            AdvisorManager.Exception(new XSystem(X));
        } finally {
            Console.WriteLine($"Press any key to close...");
            Console.ReadKey();
        }
    }
}

