using Customer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Authentication;
using TWS_Security.Sets;
using CSMFoundation.Migration.Records;

namespace Server.Controllers;


[ApiController, Route("[Controller]")]
public class ContactsController 
    : ControllerBase{

    readonly IContactService Service;

    public ContactsController(IContactService service) {
        this.Service = service;
    }

    [HttpPost("[Action]"), Auth(["ABC1","ABC2"])]

    public async Task<IActionResult> Create(Contact[] contacts) 
        => Ok(await Service.Create(contacts));
}
