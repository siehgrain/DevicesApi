using Devices.Application.DTOs;
using Devices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Interfaces
{
    public interface ICreateDeviceService
    {
        Task<Device> CreateAsync(CreateDeviceRequest request);
    }
}
