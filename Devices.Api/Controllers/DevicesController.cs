using Devices.Application.DTOs;
using Devices.Application.Services;
using Devices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers;

[ApiController]
[Route("api/devices")]
public class DevicesController : ControllerBase
{
    private readonly CreateDeviceService _createDeviceService;
    private readonly GetDevicesService _getDevicesService;

    public DevicesController(
        CreateDeviceService createService,
        GetDevicesService getDevicesService)
    {
        _createDeviceService = createService;
        _getDevicesService = getDevicesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] string? brand,
    [FromQuery] DeviceState? state)
    {
        var devices = await _getDevicesService.ExecuteAsync(brand, state);
        return Ok(devices);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateDeviceRequest request)
    {
        await _createDeviceService.CreateAsync(request);
        return Created("", null);
    }

}
