using Customer.Services.Interfaces;

using Foundation.Migrations.Records;

using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class SolutionsController
    : ControllerBase {

    readonly ISolutionsService Service;
    public SolutionsController(ISolutionsService Service) {
        this.Service = Service;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> View(MigrationViewOptions Options)
    => Ok(await Service.View(Options));
}
