using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;

[ApiController, Route("[Controller]/[Action]")]
public class SectionsController : ControllerBase {
    private readonly ISectionsService Service;
    public SectionsController(ISectionsService service) {
        Service = service;
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }
}
