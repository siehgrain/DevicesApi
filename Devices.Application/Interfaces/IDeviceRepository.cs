using Devices.Domain.Entities;
using Devices.Domain.Enums;

namespace Devices.Application.Interfaces;

public interface IDeviceRepository
{
    Task AddAsync(Device device);

    Task<Device?> GetByIdAsync(Guid id);
    Task<IEnumerable<Device>> GetAllAsync();
    Task<IEnumerable<Device>> GetByBrandAsync(string brand);
    Task<IEnumerable<Device>> GetByStateAsync(DeviceState state);

    Task UpdateAsync(Device device);
    Task DeleteAsync(Device device);
}
