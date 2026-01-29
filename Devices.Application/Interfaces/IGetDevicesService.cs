using Devices.Domain.Entities;
using Devices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Interfaces
{
    public interface IGetDevicesService
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<IEnumerable<Device>> GetByBrandAsync(string brand);
        Task<IEnumerable<Device>> GetByStateAsync(DeviceState state);
        Task<Device?> GetByIdAsync(int id);
    }
}
