﻿
using System.Net;

using Foundation.Server.Bases;

namespace Customer.Services.Exceptions;
public class XTruckAssembly : BServerTransactionException<XTruckAssemblySituation> {

    public XTruckAssembly(XTruckAssemblySituation Situation)
        : base($"", HttpStatusCode.BadRequest, null) {
        this.Situation = Situation;
        this.Advise = Situation switch {
            XTruckAssemblySituation.RequiredManufacturer => $"None Manufacturer data found.",
            XTruckAssemblySituation.RequiredPlates => $"None Plates data found.",
            XTruckAssemblySituation.ManufacturerNotExist => $"The given Manufacturer not exist",
            XTruckAssemblySituation.SitutionNotExist => $"The given Situation ID not exist",

            _ => throw new NotImplementedException()
        };
    }
}

public enum XTruckAssemblySituation {
    RequiredManufacturer,
    RequiredPlates,
    ManufacturerNotExist,
    SitutionNotExist
}
