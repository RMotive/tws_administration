using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;
using TWS_Customer.Services.Records;

namespace Server.Controllers;
/// <summary>
///     Represents the controller to perform trucks operations.
/// </summary>
[ApiController, Route("[Controller]")]
public class TrucksController : ControllerBase {
    private readonly ITrucksService Service;
    public TrucksController(ITrucksService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(Truck[] trucks)
        => Ok(await Service.Create(trucks));
}
