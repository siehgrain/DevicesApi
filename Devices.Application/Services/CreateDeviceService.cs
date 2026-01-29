using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Domain.Entities;
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
        var device = new Device(request.Name, request.Brand);

        await _repository.AddAsync(device);

        return device;
    }
}
}
