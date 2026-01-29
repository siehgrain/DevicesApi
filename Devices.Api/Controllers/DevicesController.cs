using Devices.Application.DTOs;
using Devices.Application.Services;
using Devices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly CreateDeviceService _createDeviceService;
    private readonly UpdateDeviceService _updateDeviceService;
    private readonly GetDevicesService _getDeviceService;
    private readonly DeleteDeviceService _deleteDeviceService;

    public DevicesController(
        CreateDeviceService createDeviceService,
        UpdateDeviceService updateDeviceService,
        GetDevicesService getDeviceService,
        DeleteDeviceService deleteDeviceService)
    {
        _createDeviceService = createDeviceService;
        _updateDeviceService = updateDeviceService;
        _getDeviceService = getDeviceService;
        _deleteDeviceService = deleteDeviceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceRequest dto)
    {
        var id = await _createDeviceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceDto dto)
    {
        var updated = await _updateDeviceService.ExecuteAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] UpdateDeviceDto dto)
    {
        var updated = await _updateDeviceService.ExecuteAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<DeviceResponseDto>> GetById(int id)
    {
        var device = await _getDeviceService.GetByIdAsync(id);

        if (device is null)
            return NotFound();

        return Ok(device);
    }
    [HttpGet("brand/{brand}")]
    public async Task<ActionResult<DeviceResponseDto>> GetByBrand(string brand)
    {
        var device = await _getDeviceService.GetByBrandAsync(brand);

        if (device is null)
            return NotFound();

        return Ok(device);
    }
    [HttpGet("state/{state}")]
    public async Task<ActionResult<DeviceResponseDto>> GetByState(DeviceState state)
    {
        var device = await _getDeviceService.GetByStateAsync(state);

        if (device is null)
            return NotFound();

        return Ok(device);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceResponseDto>>> GetAll()
    {
        var devices = await _getDeviceService.GetAllAsync();
        return Ok(devices);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _deleteDeviceService.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
