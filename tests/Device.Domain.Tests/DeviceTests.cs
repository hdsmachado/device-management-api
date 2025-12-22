using Device.Domain.Enums;
using Device.Domain.Exceptions;

namespace Device.Domain.Tests;

public class DeviceTests
{
    [Fact]
    public void Should_create_device_with_creation_time()
    {
        var device = new Entities.Device("iPhone", "Apple", DeviceState.Available);

        Assert.Equal("iPhone", device.Name);
        Assert.Equal("Apple", device.Brand);
        Assert.Equal(DeviceState.Available, device.State);
        Assert.True(device.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Should_update_device_when_not_in_use()
    {
        var device = new Entities.Device("iPhone", "Apple", DeviceState.Available);

        device.Update("iPhone 14", "Apple", DeviceState.Available);

        Assert.Equal("iPhone 14", device.Name);
    }

    [Fact]
    public void Should_throw_when_updating_name_or_brand_if_in_use()
    {
        var device = new Entities.Device("iPhone", "Apple", DeviceState.InUse);

        Assert.Throws<DeviceInUseException>(() =>
            device.Update("New Name", "New Brand", DeviceState.InUse));
    }

    [Fact]
    public void Should_allow_state_change_when_in_use()
    {
        var device = new Entities.Device("iPhone", "Apple", DeviceState.InUse);

        device.UpdatePartial(null, null, DeviceState.Inactive);

        Assert.Equal(DeviceState.Inactive, device.State);
    }

    [Fact]
    public void Should_throw_when_deleting_device_in_use()
    {
        var device = new Entities.Device("iPhone", "Apple", DeviceState.InUse);

        Assert.Throws<DeviceInUseException>(() =>
            device.EnsureCanBeDeleted());
    }
}
