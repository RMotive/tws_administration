using Customer;
using Customer.Contracts.Services.Interfaces;
using Customer.Models;

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
    public async Task<IActionResult> InitSession([FromBody] AccountIdentityScheme accountIdentity) {
        AccountIdentityModel RequestConvertedModel = accountIdentity.GenerateModel(); 
        ForeignSessionModel OperationResult = await Service.InitSession(RequestConvertedModel);
        return Ok(OperationResult);
    }
}
