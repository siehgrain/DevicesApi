using Devices.Application.Interfaces;
using Devices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Services
{
    public class DeleteDeviceService : IDeleteDeviceService
    {
        private readonly IDeviceRepository _repository;

        public DeleteDeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var device = await _repository.GetByIdAsync(id);

            if (device is null)
                return false;

            // Regra: device in-use não pode ser deletado
            if (device.State == DeviceState.InUse)
                throw new InvalidOperationException(
                    "Device in use cannot be deleted");

            await _repository.DeleteAsync(device);

            return true;
        }
    }
}
