using Customer.Services.Interfaces;
using Customer.Services.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using CSMFoundation.Source.Models.In;

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
    public async Task<IActionResult> View(SetViewOptions Options)
        => Ok(await Service.View(Options));

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(TruckAssembly truck)
        => Ok(await Service.Create(truck));


}
