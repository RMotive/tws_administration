using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;

using TWS_Customer.Services.Interfaces;

using TWS_Security.Sets;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class ContactsController
    : ControllerBase {
    private readonly IContactService Service;

    public ContactsController(IContactService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1", "ABC2"])]

    public async Task<IActionResult> Create(Contact[] contacts) {
        return Ok(await Service.Create(contacts));
    }
}
