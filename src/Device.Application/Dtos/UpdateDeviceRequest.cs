using Device.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Device.Application.Dtos;

public record UpdateDeviceRequest(
    [Required, StringLength(100)]
    string Name,

    [Required, StringLength(100)]
    string Brand,

    [Required]
    DeviceState State
);