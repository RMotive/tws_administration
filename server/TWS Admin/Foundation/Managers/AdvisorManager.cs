using System.Text.Json;

using Foundation.Contracts.Exceptions.Bases;

using Detail = System.Collections.Generic.KeyValuePair<string, dynamic>;
using Details = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Foundation.Managers;
public class AdvisorManager {
    private static void Restore() {
        Console.ResetColor();
    }

    private static string Label(string action) {
        return $"[{DateTime.UtcNow}] ({action}): ";
    }

    private static void PrintDetails(int depthLevel, string depthIndent, ConsoleColor color, Details details) {
        Restore();
        Console.ForegroundColor = color;
        foreach (Detail detail in details) {
            string key = detail.Key;
            dynamic content = detail.Value;
            try {
                string objectContent = JsonSerializer.Serialize(content);
                Details castedDetails = JsonSerializer.Deserialize<Details>(objectContent) ?? throw new Exception();
                string newObjectFormat = $"{depthIndent}[{key}]:";
                Console.WriteLine(newObjectFormat);
                PrintDetails(depthLevel + 1, $"{depthIndent}\t", color, castedDetails);
            } catch {
                string standardFormat = $"{depthIndent}[{key}]: {content}";
                Console.WriteLine(standardFormat);
                continue;
            }
        }
    }
    private static void Write(string Action, ConsoleColor color, string Subject, Details? Details = null) {
        string label = Label(Action);

        Restore();
        Console.ForegroundColor = color;
        Console.WriteLine($"{label}{Subject}");
        if (Details != null)
            PrintDetails(0, "\t", color, Details);
        Restore();
    }

    public static void Announce(string Subject, Details? Details = null)
    => Write("Announce", ConsoleColor.Cyan, Subject, Details);
    public static void Note(string Subject, Details? Details = null)
    => Write("Note", ConsoleColor.Blue, Subject, Details);
    public static void Success(string Subject, Details? Details = null)
    => Write("QSuccesses", ConsoleColor.DarkGreen, Subject, Details);
    public static void Exception(BException Exception)
    => Write("Exception", ConsoleColor.DarkRed, Exception.Message, Exception.GenerateAdvising());
    public static void Warning(string Subject, Details? Details = null)
    => Write("Warning", ConsoleColor.DarkYellow, Subject, Details);
}
