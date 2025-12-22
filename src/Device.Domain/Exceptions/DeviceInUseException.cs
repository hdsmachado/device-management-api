namespace Device.Domain.Exceptions;

public class DeviceInUseException : InvalidDeviceOperationException
{
    public DeviceInUseException(string message) : base(message)
    {
    }
}