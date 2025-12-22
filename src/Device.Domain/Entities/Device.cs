using Device.Domain.Enums;
using Device.Domain.Exceptions;

namespace Device.Domain.Entities;

public class Device
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Brand { get; private set; } = string.Empty;
    public DeviceState State { get; private set; }
    public DateTime CreatedAt { get; private set; }


    public Device(string name, string brand, DeviceState state)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Brand = brand ?? throw new ArgumentNullException(nameof(brand));
        State = state;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string brand, DeviceState state)
    {
        if (State == DeviceState.InUse &&
            (Name != name || Brand != brand))
        {
            throw new DeviceInUseException(
                "Name and brand cannot be updated when the device is in use.");
        }

        Name = name;
        Brand = brand;
        State = state;
    }

    public void UpdatePartial(string? name, string? brand, DeviceState? state)
    {
        if (State == DeviceState.InUse &&
            (name is not null || brand is not null))
        {
            throw new DeviceInUseException(
                "Name and brand cannot be updated when the device is in use.");
        }

        if (name is not null)
            Name = name;

        if (brand is not null)
            Brand = brand;

        if (state.HasValue)
            State = state.Value;
    }

    public void EnsureCanBeDeleted()
    {
        if (State == DeviceState.InUse)
        {
            throw new DeviceInUseException(
                "Devices in use cannot be deleted.");
        }
    }

}
