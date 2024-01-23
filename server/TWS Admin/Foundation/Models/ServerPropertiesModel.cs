using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Modelling.Bases;

namespace Foundation.Models;
public class ServerPropertiesModel
    : BModel {
    [Required]
    public string Tenant { get; private set; }
    [Required]
    public string Solution { get; private set; }
    public string IPv4 {  get; private set; }
    public string? Sign { get; private set; }
    public string? Scope { get; private set; }
    public int? Pointer { get; private set; }

    public ServerPropertiesModel(string tenant, string solution, string IPv4, string? sign, string? scope, int? pointer) {
        Tenant = tenant;
        Solution = solution;
        this.IPv4 = IPv4;
        Sign = sign;
        Scope = scope;
        Pointer = pointer;
    }
}
