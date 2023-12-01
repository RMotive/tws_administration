
namespace Server;

public class Program
{
    private static async void Initialize()
    {
        // --> Subscribing services
        await Task.Delay(3000);
        Console.WriteLine("Running server for (TWS Admin) 🛠🖥");
    }

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        // --> Initializer
        Task.Run(Initialize);

        // Add services to the container.

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

