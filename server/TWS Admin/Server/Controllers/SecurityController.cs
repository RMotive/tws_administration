using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

/// <summary>
///     Represents the controller to perform secutiry operations.
/// </summary>
[Controller, Route("[controller]")]
public class SecurityController : ControllerBase {
    public SecurityController() { }

    [HttpPost("[action]")]
    public IActionResult Login() {
        throw new NotImplementedException();
    }
}
