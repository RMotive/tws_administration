using Customer.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using CSMFoundation.Source.Models.In;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class SctController : ControllerBase {

    readonly ISctService Service;

    public SctController(ISctService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions options)
        => Ok(await Service.View(options));
}
