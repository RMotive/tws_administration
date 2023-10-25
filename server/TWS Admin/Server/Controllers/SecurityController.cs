using Customer.Services;
using Customer.Transactions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Controller, Route("[Controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly SecurityService _service;

        public SecurityController(SecurityService service)
        {
            _service = service;
        }

        [HttpPost("[Action]")]
        public IActionResult PerformLogin([FromBody] PerformLoginRequest performLoginRequest)
        => new OkObjectResult(_service.PerformLogin(performLoginRequest.ToInput()));
    }
}

