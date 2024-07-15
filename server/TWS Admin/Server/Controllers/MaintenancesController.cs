using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class MaintenancesController : ControllerBase {
    private readonly IMaintenancesService Service;

    public MaintenancesController(IMaintenancesService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions options) {
        return Ok(await Service.View(options));
    }
}
