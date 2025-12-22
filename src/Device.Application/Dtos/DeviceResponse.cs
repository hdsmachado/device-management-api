using Device.Domain.Enums;

namespace Device.Application.Dtos;

public record DeviceResponse(
    int Id,
    string Name,
    string Brand,
    DeviceState State,
    DateTime CreatedAt
);