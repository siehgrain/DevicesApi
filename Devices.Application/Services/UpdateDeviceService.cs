using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Domain.Enums;
using Devices.Domain.Exceptions;
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
            if (device is null) return false;

            bool isChangingName = dto.Name != null && dto.Name != device.Name;
            bool isChangingBrand = dto.Brand != null && dto.Brand != device.Brand;

            if (device.State == DeviceState.InUse && (isChangingName || isChangingBrand))
            {
                throw new DomainException("Name and brand cannot be updated when device is in use");
            }

            var finalName = dto.Name ?? device.Name ?? string.Empty;
            var finalBrand = dto.Brand ?? device.Brand ?? string.Empty;

            device.Update(finalName, finalBrand);

            if (dto.State.HasValue)
            {
                device.ChangeState(dto.State.Value);
            }

            await _repository.UpdateAsync(device);
            return true;
        }
    }
}
