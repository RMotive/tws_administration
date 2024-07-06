using Customer.Services.Interfaces;

using CSMFoundation.Migration.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Business.Sets;

namespace Server.Controllers;

[ApiController, Route("[Controller]")]
public class ManufacturersController : ControllerBase {

    readonly IManufacturersService Service;

    public ManufacturersController(IManufacturersService Service) {
        this.Service = Service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions Options)
        => Ok(await Service.View(Options));

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> Create(Manufacturer manufacturer)
        => Ok(await Service.Create(manufacturer));
}
