using Device.Application.Interfaces;
using Device.Domain.Entities;
using Device.Domain.Enums;
using Device.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Device.Infrastructure.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly DeviceDbContext _context;

    public DeviceRepository(DeviceDbContext context)
    {
        _context = context;
    }

    public async Task<Device.Domain.Entities.Device> AddAsync(Device.Domain.Entities.Device device, CancellationToken cancellationToken)
    {
        _context.Devices.Add(device);
        await _context.SaveChangesAsync(cancellationToken);
        return device;
    }

    public async Task<Device.Domain.Entities.Device?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Devices
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Device.Domain.Entities.Device>> GetAsync(
        string? brand,
        DeviceState? state,
        int skip,
        int take,
        CancellationToken cancellationToken)
    {
        IQueryable<Device.Domain.Entities.Device> query = _context.Devices;

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(d => d.Brand == brand);

        if (state.HasValue)
            query = query.Where(d => d.State == state.Value);

        return await query
            .OrderByDescending(d => d.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        string? brand,
        DeviceState? state,
        CancellationToken cancellationToken)
    {
        IQueryable<Device.Domain.Entities.Device> query = _context.Devices;

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(d => d.Brand == brand);

        if (state.HasValue)
            query = query.Where(d => d.State == state.Value);

        return await query.CountAsync(cancellationToken);
    }

    public async Task UpdateAsync(Device.Domain.Entities.Device device, CancellationToken cancellationToken)
    {
        _context.Devices.Update(device);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Device.Domain.Entities.Device device, CancellationToken cancellationToken)
    {
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync(cancellationToken);
    }
}