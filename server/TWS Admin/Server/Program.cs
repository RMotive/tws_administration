using System.Net;
using System.Text.Json;

using Customer;
using Customer.Contracts.Services.Interfaces;

using Foundation.Contracts.Exceptions.Bases;
using Foundation.Enumerators.Exceptions;
using Foundation.Exceptions.Servers;
using Foundation.Managers;
using Foundation.Models;
using Foundation.Models.Schemes;

namespace Server;

public class Program {
    public static ServerPropertiesModel? ServerContext { get; private set; }

    private static void LoadServerContext() {
        const string expectation = "\\Properties\\server_properties.json";
        string workingDirectory = Directory.GetCurrentDirectory();
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
        LoadServerContext();


        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services and overriding options to the container.
        builder.Services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        builder.Services.AddCors(setup => {
            setup.AddDefaultPolicy(builder => {
                builder.SetIsOriginAllowed(origin => {
                    string[] CorsPolicies = ServerContext?.Cors ?? [];
                    Uri parsedUrl = new(origin);
                    return CorsPolicies.Contains(parsedUrl.Host);
                });
            });
        }); 

        // --> Adding customer services
        {
            builder.Services.AddSingleton<ISecurityService>(new SecurityService(new()));
        }

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

