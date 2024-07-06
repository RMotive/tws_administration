using CSMFoundation.Advising.Interfaces;

using TWS_Security.Sets;

namespace Server.Models;

public class Settings
    : IAdvisingObject {
    required public string Tenant { get; init; }
    required public Solution Solution { get; init; }
    required public string Host { get; init; }
    required public string[] Listeners { get; set; }
    public string[] CORS { get; init; } = [];

    public Dictionary<string, dynamic> Advise() {
        return new() {
            {nameof(Tenant), Tenant },
            {nameof(Solution), $"{Solution.Name} (${Solution.Sign})" },
            {nameof(Host), Host },
            {nameof(Listeners), $"[{string.Join(", ", Listeners)}]" },
            {nameof(CORS), $"[{string.Join(", ", CORS)}]" },
        };
    }
}
