using System.Text.Json;

namespace CSM_Foundation.Core.Utils;
public class FileUtils {
    public static string FormatLocation(string Location) {
        string wd = Directory.GetCurrentDirectory();
        if (wd.Contains('/')) {
            Location = Location.Replace("\\", "/");
        }

        return Location;
    }
    public static TModel Deserealize<TModel>(string Location) {
        if (!File.Exists(Location)) {
            throw new FileNotFoundException(Location);
        }

        Stream fs = File.OpenRead(Location);
        TModel m = JsonSerializer.Deserialize<TModel>(fs)!;
        return m;
    }
}
