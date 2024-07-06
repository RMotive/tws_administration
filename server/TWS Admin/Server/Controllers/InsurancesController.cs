using Customer.Services.Interfaces;

using CSMFoundation.Migration.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class InsurancesController : ControllerBase {

    readonly IInsurancesService Service;

    public InsurancesController(IInsurancesService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions Options)
        => Ok(await Service.View(Options));
}