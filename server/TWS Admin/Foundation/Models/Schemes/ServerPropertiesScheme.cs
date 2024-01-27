using Foundation.Contracts.Modelling.Bases;
using Foundation.Exceptions.Modelling;

namespace Foundation.Models.Schemes;
public class ServerPropertiesScheme
    : BScheme<ServerPropertiesScheme, ServerPropertiesModel> {
    public string Tenant { get; set; }
    public string Solution { get; set; }
    public string IPv4 { get; set; }
    public string? Sign { get; set; }
    public string? Scope { get; set; }
    public int? Pointer { get; set; }

    public ServerPropertiesScheme() {
        IPv4 = String.Empty;
        Tenant = String.Empty;
        Solution = String.Empty;
    }

    protected override ServerPropertiesModel Generate() {
        List<SchemeConvertionBreakModel> breaks = [];
        if (String.IsNullOrWhiteSpace(Tenant))
            breaks.Add(new(nameof(Tenant), "is null or empty"));
        if (String.IsNullOrWhiteSpace(Solution))
            breaks.Add(new(nameof(Solution), "is null or empty"));

        if (breaks.Count > 0)
            throw new XGenerateModel<ServerPropertiesScheme, ServerPropertiesModel>(breaks);
        return new ServerPropertiesModel(Tenant, Solution, IPv4, Sign, Scope, Pointer);
    }
}
