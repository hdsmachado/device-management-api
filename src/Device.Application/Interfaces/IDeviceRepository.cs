using Device.Domain.Entities;
using Device.Domain.Enums;

namespace Device.Application.Interfaces;

public interface IDeviceRepository
{
    Task<Device.Domain.Entities.Device> AddAsync(Device.Domain.Entities.Device device, CancellationToken cancellationToken);
    Task<Device.Domain.Entities.Device?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Device.Domain.Entities.Device>> GetAsync(
        string? brand,
        DeviceState? state,
        int skip,
        int take,
        CancellationToken cancellationToken);

    Task<int> CountAsync(
        string? brand,
        DeviceState? state,
        CancellationToken cancellationToken);

    Task UpdateAsync(Device.Domain.Entities.Device device, CancellationToken cancellationToken);
    Task DeleteAsync(Device.Domain.Entities.Device device, CancellationToken cancellationToken);
}