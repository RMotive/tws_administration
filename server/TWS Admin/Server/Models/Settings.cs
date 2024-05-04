using Foundation.Advising.Interfaces;

namespace Server.Models;

public class Settings 
    : IAdvisingObject {
    required public string Tenant { get; init; }
    required public string Solution { get; init; }
    required public string Host { get; init; }
    required public string[] Listeners { get; set; }
    public string[] CORS { get; init; } = [];

    public Dictionary<string, dynamic> Advise() {
        return new() {
            {nameof(Tenant), Tenant },
            {nameof(Solution), Solution },
            {nameof(Host), Host },
            {nameof(Listeners), $"[{string.Join(", ", Listeners)}]" },
            {nameof(CORS), $"[{string.Join(", ", CORS)}]" },
        };
    }
}
