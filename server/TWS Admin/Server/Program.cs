using System.Net;
using System.Text.Json;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Servers;
using Foundation.Managers;
using Foundation.Models;
using Foundation.Models.Schemes;

using Microsoft.AspNetCore.Server.Kestrel.Core;

using Server.Middlewares;

namespace Server;

public class Program {
    const string CORS_BLOCK_MESSAGE = "Request blocked by cors, is not part of allowed hosts";
    public static ServerPropertiesModel? ServerContext { get; private set; }

    private static void LoadServerContext() {
        string expectation = "\\Properties\\server_properties.json";
        string workingDirectory = Directory.GetCurrentDirectory();
        // --> When you`re using Unix based file system.
        if (workingDirectory.Contains('/')) {
            expectation = expectation.Replace("\\", "/");
        }
        string fullPath = $"{workingDirectory}{expectation}";

        Dictionary<string, dynamic> noteDetails = new()
        {
            {"Working Directory", workingDirectory },
            {"Expected Path", fullPath },
        };
        AdvisorManager.Note("Loading server context", noteDetails);
        if (!File.Exists(fullPath))
            throw new XServerConfiguration(ServerConfigurationFailureReasons.NotFound);

        Stream fileStream = File.OpenRead(fullPath);
        ServerPropertiesScheme contextScheme
            = JsonSerializer.Deserialize<ServerPropertiesScheme>(fileStream)
            ?? throw new XServerConfiguration(ServerConfigurationFailureReasons.WrongFormat);
        try {
            string HostName = Dns.GetHostName();
            IPAddress[] Addresses = Dns.GetHostAddresses(HostName);
            contextScheme.IPv4 = Addresses.ToList().Where(I => I.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault()?.ToString() ?? "";

            ServerContext = contextScheme.GenerateModel();
            string[] Listeners = ServerContext.Listeners;
            Dictionary<string, dynamic> successDetails = new()
            {
                {"Tenant", ServerContext.Tenant },
                {"Solution", ServerContext.Solution },
                {"IPv4", ServerContext.IPv4 },
                {"Listeners", string.Join(", ", Listeners)}
            };
            AdvisorManager.Success("Server context loaded", successDetails);
        } catch (BException x) {
            AdvisorManager.Exception(x);
            throw;
        } catch {
            throw;
        }
    }

    public static void Main(string[] args) {
        AdvisorManager.Announce("Running engines (⌐■_■)");
        try {
            LoadServerContext();
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
                        string[] CorsPolicies = ServerContext?.Cors ?? [];
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
            }
            // --> Adding middleware services
            {
                builder.Services.AddSingleton(new AnalyticsMiddleware());
                builder.Services.AddSingleton(new TemplatesMiddleware());
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
                app.UseMiddleware<TemplatesMiddleware>();
            }
            app.Run();
        } catch (BException X) {
            AdvisorManager.Exception(X);
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}

