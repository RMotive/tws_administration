using Customer.Services.Interfaces;
using Foundation.Migrations.Records;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Authentication;

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
}
