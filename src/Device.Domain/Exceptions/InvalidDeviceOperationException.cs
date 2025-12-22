namespace Device.Domain.Exceptions;

public class InvalidDeviceOperationException : Exception
{
    public InvalidDeviceOperationException(string message)
        : base(message)
    {
    }
}