using Device.Application.Dtos;
using Device.Application.Interfaces;
using Device.Domain.Entities;
using Device.Domain.Enums;
using Device.Domain.Exceptions;

namespace Device.Application.Services;

public class DeviceService
{
    private readonly IDeviceRepository _repository;

    public DeviceService(IDeviceRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeviceResponse> CreateAsync(
        CreateDeviceRequest request,
        CancellationToken cancellationToken)
    {
        var device  = new Device.Domain.Entities.Device(request.Name, request.Brand, request.State);
        var created = await _repository.AddAsync(device, cancellationToken);

        return MapToResponse(created);
    }

    public async Task<DeviceResponse> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var device = await _repository.GetByIdAsync(id, cancellationToken);

        if (device is null)
            throw new DeviceNotFoundException(id);

        return MapToResponse(device);
    }

    public async Task<(IReadOnlyList<DeviceResponse> Items, int TotalCount)> GetAsync(
        string? brand,
        DeviceState? state,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var skip = (page - 1) * pageSize;

        var devices    = await _repository.GetAsync(brand, state, skip, pageSize, cancellationToken);
        var totalCount = await _repository.CountAsync(brand, state, cancellationToken);

        return (
            devices.Select(MapToResponse).ToList(),
            totalCount
        );
    }

    public async Task<DeviceResponse> UpdateAsync(
        int id,
        UpdateDeviceRequest request,
        CancellationToken cancellationToken)
    {
        var device = await GetDeviceOrThrowAsync(id, cancellationToken);

        device.Update(request.Name, request.Brand, request.State);

        await _repository.UpdateAsync(device, cancellationToken);

        return MapToResponse(device);
    }

    public async Task<DeviceResponse> PatchAsync(
        int id,
        PatchDeviceRequest request,
        CancellationToken cancellationToken)
    {
        var device = await GetDeviceOrThrowAsync(id, cancellationToken);

        device.UpdatePartial(request.Name, request.Brand, request.State);

        await _repository.UpdateAsync(device, cancellationToken);

        return MapToResponse(device);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var device = await GetDeviceOrThrowAsync(id, cancellationToken);

        device.EnsureCanBeDeleted();

        await _repository.DeleteAsync(device, cancellationToken);
    }

    private async Task<Device.Domain.Entities.Device> GetDeviceOrThrowAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var device = await _repository.GetByIdAsync(id, cancellationToken);

        if (device is null)
            throw new DeviceNotFoundException(id);

        return device;
    }

    private static DeviceResponse MapToResponse(Device.Domain.Entities.Device device) =>
        new(
            device.Id,
            device.Name,
            device.Brand,
            device.State,
            device.CreatedAt
        );

}