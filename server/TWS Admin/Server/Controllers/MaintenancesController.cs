using Customer.Services.Interfaces;

using Foundation.Migrations.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class MaintenancesController : ControllerBase {
    readonly IMaintenancesService Service;

    public MaintenancesController(IMaintenancesService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions options)
        => Ok(await Service.View(options));
}
