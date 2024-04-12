using Foundation.Contracts.Modelling.Bases;
using Foundation.Exceptions.Modelling;

namespace Foundation.Models.Schemes;
public class ServerPropertiesScheme
    : BScheme<ServerPropertiesScheme, ServerPropertiesModel> {
    public string Tenant { get; set; }
    public string Solution { get; set; }
    public string IPv4 { get; set; }
    public string[] Cors { get; set; }
    public string? Sign { get; set; }
    public string? Scope { get; set; }
    public int? Pointer { get; set; }

    public ServerPropertiesScheme() {
        IPv4 = string.Empty;
        Tenant = string.Empty;
        Solution = string.Empty;
        Cors = [];
    }

    protected override ServerPropertiesModel Generate() {
        List<SchemeConvertionBreakModel> breaks = [];
        if (string.IsNullOrWhiteSpace(Tenant))
            breaks.Add(new(nameof(Tenant), "is null or empty"));
        if (string.IsNullOrWhiteSpace(Solution))
            breaks.Add(new(nameof(Solution), "is null or empty"));

        if (breaks.Count > 0)
            throw new XModelGeneration<ServerPropertiesScheme, ServerPropertiesModel>(breaks);
        return new ServerPropertiesModel(Tenant, Solution, Cors, IPv4, Sign, Scope, Pointer);
    }
}
