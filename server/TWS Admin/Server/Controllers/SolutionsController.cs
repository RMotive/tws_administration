using Customer.Services.Interfaces;

using CSMFoundation.Migration.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Security.Sets;

namespace Server.Controllers;

[ApiController, Route("[Controller]/[Action]")]
public class SolutionsController
    : ControllerBase {

    readonly ISolutionsService Service;
    public SolutionsController(ISolutionsService Service) {
        this.Service = Service;
    }

    [HttpPost(), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions Options)
    => Ok(await Service.View(Options));

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Create(Solution[] Solutions)
    => Ok(await Service.Create(Solutions));

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Update(Solution Solution)
    => Ok(await Service.Update(Solution));
}
