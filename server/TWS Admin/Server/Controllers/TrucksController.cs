using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;
using TWS_Customer.Services.Records;

using TWS_Security.Sets;

namespace Server.Controllers;
/// <summary>
///     Represents the controller to perform trucks operations.
/// </summary>
[ApiController, Route("[Controller]/[Action]")]
public class TrucksController : ControllerBase {
    private readonly ITrucksService Service;
    public TrucksController(ITrucksService service) {
        this.Service = service;
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Create(Truck[] trucks)
        => Ok(await Service.Create(trucks));

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Update(Truck Truck, bool updatePivot = false) {
        return Ok(await Service.Update(Truck, updatePivot));
    }
}
