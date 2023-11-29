
namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        // --> Initializer
        Task.Run(async () =>
        {
            // --> Subscribing services
            await Task.Delay(2000);
            Console.WriteLine("Running services for (TWS Admin)");
        });

        // Add services to the container.

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

