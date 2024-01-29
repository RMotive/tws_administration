using Customer;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

/// <summary>
///     Represents the controller to perform secutiry operations.
/// </summary>
[ApiController, Route("[controller]")]
public class SecurityController 
    : ControllerBase {
    private readonly ISecurityService Service;

    public SecurityController(ISecurityService service) { 
        Service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginAsync([FromBody] AccountIdentityScheme accountIdentity)
    => Ok(await Service.InitSession(accountIdentity.GenerateModel()));
}
