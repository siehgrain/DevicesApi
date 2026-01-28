using Devices.Application.DTOs;
using Devices.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers;

[ApiController]
[Route("api/devices")]
public class DevicesController : ControllerBase
{
    private readonly CreateDeviceService _createDeviceService;

    public DevicesController(CreateDeviceService createDeviceService)
    {
        _createDeviceService = createDeviceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDeviceRequest request)
    {
        await _createDeviceService.ExecuteAsync(request);
        return Created("", null);
    }
}
