using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class ManufacturersController : ControllerBase {
    private readonly IManufacturersService Service;

    public ManufacturersController(IManufacturersService Service) {
        this.Service = Service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(Manufacturer manufacturer) {
        return Ok(await Service.Create(manufacturer));
    }
}
