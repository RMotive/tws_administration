
using System.Net;

using Foundation.Server.Bases;

namespace Customer.Services.Exceptions;
public class XTruckAssembly : BServerTransactionException<XTrcukAssemblySituation> {

    public XTruckAssembly(XTrcukAssemblySituation Situation)
        : base($"", HttpStatusCode.BadRequest, null) {
        this.Situation = Situation;
        this.Advise = Situation switch {
            XTrcukAssemblySituation.Required_Manufacturer => $"None Manufacturer data found.",
            XTrcukAssemblySituation.Required_Plates => $"None Plates data found.",
            XTrcukAssemblySituation.Manufacturer_Not_Exist => $"The given Manufacturer not exist",
            _ => throw new NotImplementedException()
        };
    }
}

public enum XTrcukAssemblySituation {
    Required_Manufacturer,
    Required_Plates,
    Manufacturer_Not_Exist
}
