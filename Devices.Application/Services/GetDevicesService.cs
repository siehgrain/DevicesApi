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

    public async Task<IEnumerable<Device>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Device>> GetByBrandAsync(string brand)
    {
        return await _repository.GetByBrandAsync(brand);
    }
    public async Task<Device?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Device>> GetByStateAsync(DeviceState state)
    {
        return await _repository.GetByStateAsync(state);
    }
}
