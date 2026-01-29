using Devices.Application.Interfaces;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Devices.Infrastructure.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly DevicesDbContext _context;

    public DeviceRepository(DevicesDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Device device)
    {
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
    }

    public async Task<Device?> GetByIdAsync(int id)
    {
        return await _context.Devices.FindAsync(id);
    }

    public async Task<IEnumerable<Device>> GetAllAsync()
    {
        return await _context.Devices.ToListAsync();
    }

    public async Task<IEnumerable<Device>> GetByBrandAsync(string brand)
    {
        return await _context.Devices
            .Where(d => d.Brand == brand)
            .ToListAsync();
    }

    public async Task<IEnumerable<Device>> GetByStateAsync(DeviceState state)
    {
        return await _context.Devices
            .Where(d => d.State == state)
            .ToListAsync();
    }

    public async Task UpdateAsync(Device device)
    {
        _context.Devices.Update(device);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Device device)
    {
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
    }
}
