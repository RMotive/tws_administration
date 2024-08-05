using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;

[ApiController, Route("[Controller]/[Action]")]
public class HPTrucksController : ControllerBase {
    private readonly  IHPTruckService Service;

    public HPTrucksController(IHPTruckService service) {
        Service = service;
    }


    [HttpPost(), Auth([])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Create(HPTruck[] HPTrucks)
        => Ok(await Service.Create(HPTrucks));

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Update(HPTruck Truck) {
        return Ok(await Service.Update(Truck));
    }
}
