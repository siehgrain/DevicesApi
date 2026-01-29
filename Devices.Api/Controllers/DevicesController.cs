using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Application.Services;
using Devices.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly ICreateDeviceService _createDeviceService;
    private readonly IUpdateDeviceService _updateDeviceService;
    private readonly IGetDevicesService _getDeviceService;
    private readonly IDeleteDeviceService _deleteDeviceService;

    public DevicesController(
        ICreateDeviceService createDeviceService,
        IUpdateDeviceService updateDeviceService,
        IGetDevicesService getDeviceService,
        IDeleteDeviceService deleteDeviceService)
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
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceDto dto)
    {
        var updated = await _updateDeviceService.ExecuteAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] UpdateDeviceDto dto)
    {
        var updated = await _updateDeviceService.ExecuteAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }
    [Authorize]
    [HttpGet("id/{id}")]
    public async Task<ActionResult<DeviceResponseDto>> GetById(int id)
    {
        var device = await _getDeviceService.GetByIdAsync(id);

        if (device is null)
            return NotFound();

        return Ok(device);
    }
    [Authorize]
    [HttpGet("brand/{brand}")]
    public async Task<ActionResult<DeviceResponseDto>> GetByBrand(string brand)
    {
        var device = await _getDeviceService.GetByBrandAsync(brand);

        if (device is null)
            return NotFound();

        return Ok(device);
    }
    [Authorize]
    [HttpGet("state/{state}")]
    public async Task<ActionResult<DeviceResponseDto>> GetByState(DeviceState state)
    {
        var device = await _getDeviceService.GetByStateAsync(state);

        if (device is null)
            return NotFound();

        return Ok(device);
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceResponseDto>>> GetAll()
    {
        var devices = await _getDeviceService.GetAllAsync();
        return Ok(devices);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _deleteDeviceService.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
