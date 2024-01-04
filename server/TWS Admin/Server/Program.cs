
using Foundation.Contracts.Exceptions;
using Foundation.Exceptions.Servers;
using Foundation.Managers;
using Foundation.Models;
using Foundation.Models.Schemes;

using System.Text.Json;

namespace Server;

public class Program {
    public static ServerPropertiesModel ServerContext {
        get {
            return ServerContext;
        }
        private set {
            ServerContext = value;
        }
    }

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
            throw new XServerContext(XServerContext.Reason.NotFound);

        Stream fileStream = File.OpenRead(fullPath);
        ServerPropertiesScheme contextScheme
            = JsonSerializer.Deserialize<ServerPropertiesScheme>(fileStream)
            ?? throw new XServerContext(XServerContext.Reason.WrongFormat);
        try {
            ServerContext = contextScheme.GenerateModel();
            Dictionary<string, dynamic> successDetails = new()
            {
                {"Tenant", ServerContext.Tenant },
                {"Solution", ServerContext.Solution },
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
        AdvisorManager.Announce("Loading server dependencies to run...");
        LoadServerContext();

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

