using Device.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Device.Application.Dtos;

public record CreateDeviceRequest(
    [Required, StringLength(100)]
    string Name,

    [Required, StringLength(100)]
    string Brand,

    [Required]
    DeviceState State
);