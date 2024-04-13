using System.ComponentModel.DataAnnotations;

using Foundation.Contracts.Modelling.Bases;

namespace Foundation.Models;
public class SchemeConvertionBreakModel
    : BModel {
    [Required]
    public new string Property { get; private set; }
    [Required]
    public string Reason { get; private set; }

    public SchemeConvertionBreakModel(string property, string reason) {
        Property = property;
        Reason = reason;
    }
}
