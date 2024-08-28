using System.Text.Json;
using System.Text.Json.Serialization;

using CSM_Foundation.Advisor.Interfaces;
using CSM_Foundation.Advisor.Managers;
using CSM_Foundation.Core.Exceptions;
using CSM_Foundation.Core.Utils;
using CSM_Foundation.Server.Utils;
using CSM_Foundation.Source.Interfaces;

using Server.Managers;
using Server.Middlewares;
using Server.Models;

using TWS_Business;
using TWS_Business.Depots;

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
            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IncludeFields = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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
                builder.Services.AddDbContext<TWSBusinessSource>();

                // --> Depots
                builder.Services.AddScoped<SolutionsDepot>();
                builder.Services.AddScoped<AccountsDepot>();
                builder.Services.AddScoped<AddressesDepot>();
                builder.Services.AddScoped<UsdotsDepot>();
                builder.Services.AddScoped<CarriersDepot>();
                builder.Services.AddScoped<ApproachesDepot>();
                builder.Services.AddScoped<ContactsDepot>();
                builder.Services.AddScoped<ManufacturersDepot>();
                builder.Services.AddScoped<SituationsDepot>();
                builder.Services.AddScoped<PlatesDepot>();
                builder.Services.AddScoped<TruckDepot>();
                builder.Services.AddScoped<InsurancesDepot>();
                builder.Services.AddScoped<SctsDepot>();
                builder.Services.AddScoped<MaintenacesDepot>();
                builder.Services.AddScoped<StatusesDepot>();
                builder.Services.AddScoped<AddressesDepot>();
                builder.Services.AddScoped<ApproachesDepot>();
                builder.Services.AddScoped<CarriersDepot>();
                builder.Services.AddScoped<AxesDepot>();
                builder.Services.AddScoped<DriversDepot>();
                builder.Services.AddScoped<DriversCommonsDepot>();
                builder.Services.AddScoped<DriversExternalsDepot>();
                builder.Services.AddScoped<EmployeesDepot>();
                builder.Services.AddScoped<SectionsDepot>();
                builder.Services.AddScoped<LocationsDepot>();
                builder.Services.AddScoped<LoadTypesDepot>();
                builder.Services.AddScoped<LocationsDepot>();
                builder.Services.AddScoped<TrailerClassesDepot>();
                builder.Services.AddScoped<TrailersCommonsDepot>();
                builder.Services.AddScoped<TrailersExternalsDepot>();
                builder.Services.AddScoped<TrailersDepot>();
                builder.Services.AddScoped<IdentificationsDepot>();
                builder.Services.AddScoped<TrucksExternalsDepot>();
                builder.Services.AddScoped<TrucksCommonsDepot>();

                builder.Services.AddScoped<YardLogsDepot>();

                builder.Services.AddScoped<TrucksHDepot>();
                // --> Services
                builder.Services.AddScoped<ISolutionsService, SolutionsService>();
                builder.Services.AddScoped<ISecurityService, SecurityService>();
                builder.Services.AddScoped<IManufacturersService, ManufacturersService>();
                builder.Services.AddScoped<ISituationsService, SituationsService>();
                builder.Services.AddScoped<IPlatesService, PlatesServices>();
                builder.Services.AddScoped<ITrucksService, TrucksService>();
                builder.Services.AddScoped<ITrucksExternalsService, TrucksExternalsService>();
                builder.Services.AddScoped<IContactService, ContactService>();
                builder.Services.AddScoped<IDriversService, DriversService>();
                builder.Services.AddScoped<IDriversExternalsService, DriversExternalsService>();
                builder.Services.AddScoped<ITrucksExternalsService, TrucksExternalsService>();
                builder.Services.AddScoped<ITrailersService, TrailersService>();
                builder.Services.AddScoped<ITrailersExternalsService, TrailersExternalsService>();
                builder.Services.AddScoped<ILoadTypesService, LoadTypesService>();
                builder.Services.AddScoped<ISectionsService, SectionsService>();
                builder.Services.AddScoped<IYardLogsService, YardLogsService>();

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

