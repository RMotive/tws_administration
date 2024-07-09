using Customer.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using CSMFoundation.Source.Models.In;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class InsurancesController : ControllerBase {

    readonly IInsurancesService Service;

    public InsurancesController(IInsurancesService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions Options)
        => Ok(await Service.View(Options));
}