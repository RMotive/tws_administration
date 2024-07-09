using Customer.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class SituationsController : ControllerBase {
    readonly ISituationsService Service;

    public SituationsController(ISituationsService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions Options)
        => Ok(await Service.View(Options));

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(Situation situation)
        => Ok(await Service.Create(situation));
}
