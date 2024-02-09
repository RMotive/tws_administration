using Foundation.Contracts.Exceptions.Bases;

namespace Foundation.Managers;
public class AdvisorManager {
    private static void Restore() {
        Console.ResetColor();
    }

    private static string Label(string action) {
        return $"[{DateTime.UtcNow}] ({action}): ";
    }

    private static void Write(string Action, ConsoleColor color, string Subject, Dictionary<string, dynamic>? Details = null) {
        string label = Label(Action);

        Restore();
        Console.ForegroundColor = color;
        Console.WriteLine($"{label}{Subject}");
        foreach (KeyValuePair<string, dynamic> entry in Details ?? []) {
            Console.WriteLine($"    \t({entry.Key}): [{entry.Value}]");
        }
        Restore();
    }

    public static void Announce(string Subject, Dictionary<string, dynamic>? Details = null)
    => Write("Announce", ConsoleColor.Cyan, Subject, Details);
    public static void Note(string Subject, Dictionary<string, dynamic>? Details = null)
    => Write("Note", ConsoleColor.Blue, Subject, Details);
    public static void Success(string Subject, Dictionary<string, dynamic>? Details = null)
    => Write("Success", ConsoleColor.DarkGreen, Subject, Details);
    public static void Exception(BException Exception) {
        throw new NotImplementedException();
    }
}
