using System.Text.Json;
using System.Text.Json.Serialization;

using CSMFoundation.Advising.Interfaces;
using CSMFoundation.Advising.Managers;
using CSMFoundation.Core.Exceptions;
using CSMFoundation.Migration.Interfaces;
using CSMFoundation.Server.Utils;
using CSMFoundation.Utils;

using Customer.Services;
using Customer.Services.Interfaces;

using Microsoft.AspNetCore.Server.Kestrel.Core;

using Server.Managers;
using Server.Middlewares;
using Server.Models;

using TWS_Security;
using TWS_Security.Depots;

namespace Server;

public partial class Program {
    const string SETTINGS_LOCATION = "\\Properties\\server_properties.json";
    const string CORS_BLOCK_MESSAGE = "Request blocked by cors, is not part of allowed hosts";

    static IMigrationDisposer? Disposer;
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
                // --> Application
                builder.Services.AddSingleton<AnalyticsMiddleware>();
                builder.Services.AddSingleton<FramingMiddleware>();
                builder.Services.AddSingleton<AdvisorMiddleware>();
                builder.Services.AddSingleton<DispositionMiddleware>();
                builder.Services.AddSingleton<IMigrationDisposer, DispositionManager>();

                // --> Sources contexts
                builder.Services.AddDbContext<TWSSecuritySource>();

                // --> Depots
                builder.Services.AddScoped<SolutionsDepot>();
                builder.Services.AddScoped<AccountsDepot>();

                // --> Services
                builder.Services.AddScoped<ISolutionsService, SolutionsService>();
                builder.Services.AddScoped<ISecurityService, SecurityService>();

                builder.Services.AddSingleton<IManufacturersService>(new ManufacturersService(new()));
                builder.Services.AddSingleton<IInsurancesService>(new InsuranceService(new()));
                builder.Services.AddSingleton<IMaintenancesService>(new MaintenanceService(new()));
                builder.Services.AddSingleton<ISctService>(new SctService(new()));
                builder.Services.AddSingleton<ISituationsService>(new SituationsService(new()));
                builder.Services.AddSingleton<IPlatesService>(new PlatesServices(new()));
                builder.Services.AddSingleton<IContactService>(new ContactService(new()));
                builder.Services.AddSingleton<ITrucksService>(new TrucksService(new(), new(), new(), new(), new(), new(), new()));
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

            Disposer = app.Services.GetService<IMigrationDisposer>()
                ?? throw new Exception("Required disposer service");
            app.Lifetime.ApplicationStopping.Register(OnProcessExit);
            app.UseCors();


            AdvisorManager.Announce($"Server ready to listen 🚀");
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
        Disposer?.Dispose();
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

