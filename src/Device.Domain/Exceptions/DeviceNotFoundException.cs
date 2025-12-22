namespace Device.Domain.Exceptions;

public class DeviceNotFoundException : Exception
{
    public DeviceNotFoundException(int deviceId)
        : base($"Device with id {deviceId} was not found.")
    {
    }
}