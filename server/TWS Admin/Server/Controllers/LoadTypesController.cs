﻿using CSM_Foundation.Source.Models.Options;

using Microsoft.AspNetCore.Mvc;

using Server.Controllers.Authentication;
using TWS_Customer.Services.Interfaces;

namespace Server.Controllers;

[ApiController, Route("[Controller]/[Action]")]
public class LoadTypesController : ControllerBase {
    private readonly ILoadTypesService Service;
    public LoadTypesController(ILoadTypesService service) {
        Service = service;
    }

    [HttpPost(), Auth([])]
    public async Task<IActionResult> View(SetViewOptions Options) {
        return Ok(await Service.View(Options));
    }
}