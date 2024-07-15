using Microsoft.AspNetCore.Mvc;

using TWS_Customer.Services.Interfaces;
using TWS_Customer.Services.Records;

namespace Server.Controllers;

/// <summary>
///     Represents the controller to perform secutiry operations.
/// </summary>
[ApiController, Route("[Controller]")]
public class SecurityController
    : ControllerBase {
    private readonly ISecurityService Service;
    public SecurityController(ISecurityService Service) {
        this.Service = Service;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> Authenticate([FromBody] Credentials Credentials) {
        return Ok(await Service.Authenticate(Credentials));
    }
}
