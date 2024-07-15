using System.Text.Json;
using System.Text.Json.Serialization;

using CSM_Foundation.Advisor.Interfaces;
using CSM_Foundation.Advisor.Managers;
using CSM_Foundation.Core.Exceptions;
using CSM_Foundation.Core.Utils;
using CSM_Foundation.Server.Utils;
using CSM_Foundation.Source.Interfaces;

using Microsoft.AspNetCore.Server.Kestrel.Core;

using Server.Managers;
using Server.Middlewares;
using Server.Models;

using TWS_Customer.Services;
using TWS_Customer.Services.Interfaces;

using TWS_Security;
using TWS_Security.Depots;

namespace Server;

public partial class Program {
    private const string SETTINGS_LOCATION = "\\Properties\\server_properties.json";
    private const string CORS_BLOCK_MESSAGE = "Request blocked by cors, is not part of allowed hosts";
    private static IMigrationDisposer? Disposer;
    private static Settings? SettingsStore { get; set; }
    public static Settings Settings => SettingsStore ??= RetrieveSettings();

    private static void Main(string[] args) {
        Configure();
        AdvisorManager.Announce("Running engines (⌐■_■)");

        try {
            Settings s = Settings;

            AdvisorManager.Success("Server settings retrieved", s);

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            // Add services and overriding options to the container.
            _ = builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IncludeFields = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            _ = builder.Services.Configure<KestrelServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });
            _ = builder.Services.AddCors(setup => {
                setup.AddDefaultPolicy(builder => {
                    _ = builder.AllowAnyHeader();
                    _ = builder.AllowAnyMethod();
                    _ = builder.SetIsOriginAllowed(origin => {
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
                _ = builder.Services.AddSingleton<AnalyticsMiddleware>();
                _ = builder.Services.AddSingleton<FramingMiddleware>();
                _ = builder.Services.AddSingleton<AdvisorMiddleware>();
                _ = builder.Services.AddSingleton<DispositionMiddleware>();
                _ = builder.Services.AddSingleton<IMigrationDisposer, DispositionManager>();

                // --> Sources contexts
                _ = builder.Services.AddDbContext<TWSSecuritySource>();

                // --> Depots
                _ = builder.Services.AddScoped<SolutionsDepot>();
                _ = builder.Services.AddScoped<AccountsDepot>();
                _ = builder.Services.AddScoped<ContactsDepot>();

                // --> Services
                _ = builder.Services.AddScoped<ISolutionsService, SolutionsService>();
                _ = builder.Services.AddScoped<ISecurityService, SecurityService>();




                // --> TODO: Follow dependency pattern and modify this ones.
                _ = builder.Services.AddSingleton<IManufacturersService>(new ManufacturersService(new()));
                _ = builder.Services.AddSingleton<IInsurancesService>(new InsuranceService(new()));
                _ = builder.Services.AddSingleton<IMaintenancesService>(new MaintenanceService(new()));
                _ = builder.Services.AddSingleton<ISctService>(new SctService(new()));
                _ = builder.Services.AddSingleton<ISituationsService>(new SituationsService(new()));
                _ = builder.Services.AddSingleton<IPlatesService>(new PlatesServices(new()));
                _ = builder.Services.AddSingleton<IContactService>(new ContactService(new()));
                _ = builder.Services.AddSingleton<ITrucksService>(new TrucksService(new(), new(), new(), new(), new(), new(), new()));
            }
            WebApplication app = builder.Build();
            _ = app.MapControllers();
            // --> Injecting middlewares to server
            {
                _ = app.UseMiddleware<AnalyticsMiddleware>();
                _ = app.UseMiddleware<AdvisorMiddleware>();
                _ = app.UseMiddleware<FramingMiddleware>();
                _ = app.UseMiddleware<DispositionMiddleware>();
            }

            Disposer = app.Services.GetService<IMigrationDisposer>()
                ?? throw new Exception("Required disposer service");
            _ = app.Lifetime.ApplicationStopping.Register(OnProcessExit);
            _ = app.UseCors();


            AdvisorManager.Announce($"Server ready to listen 🚀");
            app.Run();
        } catch (Exception X) when (X is IAdvisingException AX) {
            AdvisorManager.Exception(AX);
            throw;
        } catch (Exception X) {
            AdvisorManager.Exception(new XSystem(X));
        } finally {
            Console.WriteLine($"Press any key to close...");
            _ = Console.ReadKey();
        }
    }

    private static void Configure() {
        Console.Title = $"{Settings.Solution.Name} | {Settings.Host}";
    }

    private static void OnProcessExit() {
        AdvisorManager.Announce("Disposing quality context records");
        Disposer?.Dispose();
    }

    private static Settings RetrieveSettings() {
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

