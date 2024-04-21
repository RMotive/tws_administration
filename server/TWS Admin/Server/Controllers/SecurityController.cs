using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

/// <summary>
///     Represents the controller to perform secutiry operations.
/// </summary>
[ApiController, Route("[controller]")]
public class SecurityController
    : ControllerBase {
    //private readonly ISecurityService Service;

    public SecurityController() { }
}
