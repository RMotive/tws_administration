using Customer.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Business.Sets;
using CSMFoundation.Source.Models.In;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class PlatesController : ControllerBase {

    readonly IPlatesService Service;

    public PlatesController(IPlatesService service) {
        Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions Options)
        => Ok(await Service.View(Options));

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(Plate plate)
        => Ok(await Service.Create(plate));
}
