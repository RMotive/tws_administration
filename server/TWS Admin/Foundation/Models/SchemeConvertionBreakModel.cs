using Foundation.Contracts.Modelling.Bases;

using System.ComponentModel.DataAnnotations;

namespace Foundation.Models;
public class SchemeConvertionBreakModel
    : BModel {
    [Required]
    public string Property { get; private set; }
    [Required]
    public string Reason { get; private set; }

    public SchemeConvertionBreakModel(string property, string reason) {
        Property = property;
        Reason = reason;
    }
}
