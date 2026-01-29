using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Services
{
    public class UpdateDeviceService : IUpdateDeviceService
    {
        private readonly IDeviceRepository _repository;

        public UpdateDeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(int id, UpdateDeviceDto dto)
        {
            var device = await _repository.GetByIdAsync(id);

            if (device is null)
                return false;

            // Regra: name e brand não podem mudar se estiver in-use
            if (device.State == DeviceState.InUse &&(dto.Name is not null || dto.Brand is not null))
            {
                throw new InvalidOperationException(
                    "Name and brand cannot be updated when device is in use");
            }

            device.Update(dto.Name ?? device.Name,dto.Brand ?? device.Brand);

            await _repository.UpdateAsync(device);

            return true;
        }
    }
}
