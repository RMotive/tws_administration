using Customer.Services.Interfaces;

using CSMFoundation.Migration.Records;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class SctController : ControllerBase {

    readonly ISctService Service;

    public SctController(ISctService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]
    public async Task<IActionResult> View(MigrationViewOptions options)
        => Ok(await Service.View(options));
}
