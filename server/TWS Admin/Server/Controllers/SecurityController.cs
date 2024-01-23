using Customer;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

/// <summary>
///     Represents the controller to perform secutiry operations.
/// </summary>
[Controller, Route("[controller]")]
public class SecurityController : ControllerBase {
    private readonly ISecurityService Service;

    public SecurityController(ISecurityService service) { 
        Service = service;
    }

    [HttpGet("[action]")]
    public IActionResult Login() 
    => new OkObjectResult(Service.InitSession());
}
