using Foundation.Contracts.Modelling.Bases;
using Foundation.Exceptions.Modelling;

namespace Foundation.Models.Schemes;
public class ServerPropertiesScheme
    : BScheme<ServerPropertiesModel> {
    public string Tenant { get; set; }
    public string Solution { get; set; }
    public string? Sign { get; set; }
    public string? Scope { get; set; }
    public int? Pointer { get; set; }

    public ServerPropertiesScheme() {
        Tenant = String.Empty;
        Solution = String.Empty;
    }

    public override ServerPropertiesModel GenerateModel() {
        List<SchemeConvertionBreakModel> breaks = [];
        if (String.IsNullOrEmpty(Tenant))
            breaks.Add(new(nameof(Tenant), "is null or empty"));
        if (String.IsNullOrEmpty(Solution))
            breaks.Add(new(nameof(Tenant), "is null or empty"));

        if (breaks.Count > 0)
            throw new XGenerateModel<ServerPropertiesScheme, ServerPropertiesModel>(breaks);
        return new ServerPropertiesModel(Tenant, Solution, Sign, Scope, Pointer);
    }
}
