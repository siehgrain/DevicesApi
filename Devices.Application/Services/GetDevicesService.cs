using Devices.Application.Interfaces;
using Devices.Domain.Entities;
using Devices.Domain.Enums;

namespace Devices.Application.Services;

public class GetDevicesService
{
    private readonly IDeviceRepository _repository;

    public GetDevicesService(IDeviceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Device>> ExecuteAsync(
        string? brand,
        DeviceState? state)
    {
        if (!string.IsNullOrWhiteSpace(brand))
            return await _repository.GetByBrandAsync(brand);

        if (state.HasValue)
            return await _repository.GetByStateAsync(state.Value);

        return await _repository.GetAllAsync();
    }
}
