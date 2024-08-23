using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using TWS_Business.Sets;

using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;

[ApiController, Route("[Controller]/[Action]")]
public class YardLogsController : ControllerBase {
    private readonly IYardLogsService Service;
    public YardLogsController(IYardLogsService service) {
        Service = service;
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Create(YardLog[] yardLogs)
        => Ok(await Service.Create(yardLogs));

    [HttpPost(), Auth([])]
    public async Task<IActionResult> Update(YardLog yardLogs, bool updatePivot = false) {
        return Ok(await Service.Update(yardLogs, updatePivot));
    }

}
