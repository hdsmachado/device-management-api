using Device.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Device.Application.Dtos;

public record PatchDeviceRequest(
    [StringLength(100)]
    string? Name,

    [StringLength(100)]
    string? Brand,

    DeviceState? State
);