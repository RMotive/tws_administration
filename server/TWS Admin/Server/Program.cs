using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Customer.Services;
using Customer.Services.Interfaces;

using Foundation.Advising.Interfaces;
using Foundation.Advising.Managers;
using Foundation.Server.Utils;
using Foundation.Shared.Exceptions;
using Foundation.Utils;

using Microsoft.AspNetCore.Server.Kestrel.Core;

using Server.Managers;
using Server.Middlewares;
using Server.Models;

namespace Server;

public partial class Program {
    private enum CtrlType {
        CTRL_C_EVENT = 0,
        CTRL_BREAK_EVENT = 1,
        CTRL_CLOSE_EVENT = 2,
        CTRL_LOGOFF_EVENT = 5,
        CTRL_SHUTDOWN_EVENT = 6
    }

    const string SETTINGS_LOCATION = "\\Properties\\server_properties.json";
    const string CORS_BLOCK_MESSAGE = "Request blocked by cors, is not part of allowed hosts";

    static readonly DispositionManager Disposer = new();

    private static Settings? SettingsStore { get; set; }
    public static Settings Settings { get { return SettingsStore ??= RetrieveSettings(); } }


    static void Main(string[] args) {
        Configure();
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
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            builder.Services.Configure<KestrelServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });
            builder.Services.AddCors(setup => {
                setup.AddDefaultPolicy(builder => {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.SetIsOriginAllowed(origin => {
                        string[] CorsPolicies = Settings.CORS;
                        Uri parsedUrl = new(origin);

                        bool isCorsAllowed = CorsPolicies.Contains(parsedUrl.Host);
                        if (!isCorsAllowed) {
                            AdvisorManager.Warning(CORS_BLOCK_MESSAGE, new() {
                                {nameof(isCorsAllowed), isCorsAllowed},
                                {nameof(parsedUrl), parsedUrl}
                            });
                        }
                        return isCorsAllowed;
                    });
                });
            });

            // --> Adding customer services
            {
                builder.Services.AddSingleton<IManufacturersService>(new ManufacturersService(new()));
                builder.Services.AddSingleton(Disposer);
                builder.Services.AddTransient<ISolutionsService>((IServiceProvider sp) => new SolutionsService(new(Disposer.Push)));
                builder.Services.AddTransient<ISecurityService>((SP) => new SecurityService(new(Disposer.Push)));
                builder.Services.AddSingleton<IInsurancesService>(new InsuranceService(new()));
                builder.Services.AddSingleton<IMaintenancesService>(new MaintenanceService(new()));
                builder.Services.AddSingleton<ISctService>(new SctService(new()));
                builder.Services.AddSingleton<ISituationsService>(new SituationsService(new()));
                builder.Services.AddSingleton<IPlatesService>(new PlatesServices(new()));
                builder.Services.AddSingleton<ITrucksService>(new TrucksService(new(), new(), new(), new(), new(), new(), new()));
            }
            // --> Adding middleware services
            {
                builder.Services.AddSingleton(new AnalyticsMiddleware());
                builder.Services.AddSingleton(new FramingMiddleware());
                builder.Services.AddSingleton(new AdvisorMiddleware());
                builder.Services.AddSingleton(new DispositionMiddleware(Disposer));
            }
            WebApplication app = builder.Build();
            app.MapControllers();
            // --> Injecting middlewares to server
            {
                app.UseMiddleware<AnalyticsMiddleware>();
                app.UseMiddleware<AdvisorMiddleware>();
                app.UseMiddleware<FramingMiddleware>();
                app.UseMiddleware<DispositionMiddleware>();
            }

            app.Lifetime.ApplicationStopping.Register(OnProcessExit);
            app.UseCors();
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


    static void Configure() {
        Console.Title = $"{Settings.Solution.Name} | {Settings.Host}";
    }
    static void OnProcessExit() {
        AdvisorManager.Announce("Disposing quality context records");
        Disposer.Dispose();
    }
    static Settings RetrieveSettings() {
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
}

