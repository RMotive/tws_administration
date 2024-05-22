using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Authentication;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class SituationsController : ControllerBase {
    readonly ISituationsService Service;

    public SituationsController(ISituationsService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions Optios)
        => Ok(await Service.View(Optios));
}
