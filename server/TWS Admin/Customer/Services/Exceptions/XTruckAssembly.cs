
using Foundation.Server.Bases;
using System.Net;

namespace Customer.Services.Exceptions;
public class XTruckAssembly: BServerTransactionException<XTrcukAssemblySituation> {

    public XTruckAssembly(XTrcukAssemblySituation Situation) 
        : base($"", HttpStatusCode.BadRequest, null){
        this.Situation = Situation;
        this.Advise = Situation switch {
            XTrcukAssemblySituation.Required_Manufacturer => $"None Manufacturer data found.",
            XTrcukAssemblySituation.Multiple_Manufacturer_Input => $"Multiple Manufacturer Values. Enter only the [Manufacturer] field or [ManufacturerPointer] but not both.",
            XTrcukAssemblySituation.Required_Plates => $"None Plates data found.",
            XTrcukAssemblySituation.Multiple_Plates_Input => $"Multiple Plates Values. Enter only the [Plates] field or [PlatesPointer] but not both.",
            _ => throw new NotImplementedException()
        };
    }
}

public enum XTrcukAssemblySituation {
    Multiple_Manufacturer_Input,
    Required_Manufacturer,
    Multiple_Plates_Input,
    Required_Plates
}
