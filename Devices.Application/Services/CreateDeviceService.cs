using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Domain.Entities;
using Devices.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Services
{
    public class CreateDeviceService : ICreateDeviceService
    {
        private readonly IDeviceRepository _repository;

        public CreateDeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Device> CreateAsync(CreateDeviceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new DomainException("Device name cannot be null or empty");

            if (string.IsNullOrWhiteSpace(request.Brand))
                throw new DomainException("Device brand cannot be null or empty");
            var device = new Device(request.Name, request.Brand);

            await _repository.AddAsync(device);

            return device;
        }
    }
}
