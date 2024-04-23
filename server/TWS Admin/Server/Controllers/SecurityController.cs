using Customer.Services.Interfaces;
using Customer.Services.Records;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

/// <summary>
///     Represents the controller to perform secutiry operations.
/// </summary>
[ApiController, Route("[controller]")]
public class SecurityController
    : ControllerBase {
    
    
    readonly ISecurityService Service;
    public SecurityController(ISecurityService Service) {
        this.Service = Service;
    }

    [HttpPost("[Action]")]
    public async Task<IActionResult> Authenticate(Credentials Credentials)
    => Ok(await Service.Authenticate(Credentials));
}
