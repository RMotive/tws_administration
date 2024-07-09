using Customer.Services.Interfaces;

using Foundation.Migrations.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Security.Sets;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class SolutionsController
    : ControllerBase {

    readonly ISolutionsService Service;
    public SolutionsController(ISolutionsService Service) {
        this.Service = Service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions Options)
    => Ok(await Service.View(Options));

    [HttpPost("[Action]"), Auth([])]
    public async Task<IActionResult> Create(Solution[] Solutions)
    => Ok(await Service.Create(Solutions));
}
