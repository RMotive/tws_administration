using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class SctController : ControllerBase {
    private readonly ISctService Service;

    public SctController(ISctService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions options) {
        return Ok(await Service.View(options));
    }
}
