using Customer.Services.Interfaces;
using Customer.Services.Records;

using Foundation.Migrations.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using TWS_Business.Sets;

namespace Server.Controllers;
/// <summary>
///     Represents the controller to perform trucks operations.
/// </summary>
[ApiController, Route("[Controller]")]
public class TrucksController : ControllerBase {

    readonly ITrucksService Service;
    public TrucksController(ITrucksService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions Options)
        => Ok(await Service.View(Options));

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(Truck[] trucks)
        => Ok(await Service.Create(trucks));


}
