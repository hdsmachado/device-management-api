using Device.Application.Dtos;
using Device.Application.Interfaces;
using Device.Application.Services;
using Device.Domain.Entities;
using Device.Domain.Enums;
using Device.Domain.Exceptions;
using Moq;
using Xunit;

namespace Device.Application.Tests;

public class DeviceServiceTests
{
    private readonly Mock<IDeviceRepository> _repositoryMock;
    private readonly DeviceService _service;

    public DeviceServiceTests()
    {
        _repositoryMock = new Mock<IDeviceRepository>();
        _service = new DeviceService(_repositoryMock.Object);
    }

    [Fact]
    public async Task Should_create_device()
    {
        var request = new CreateDeviceRequest("iPhone", "Apple", DeviceState.Available);

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Device.Domain.Entities.Device>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Device.Domain.Entities.Device d, CancellationToken _) => d);

        var result = await _service.CreateAsync(request, CancellationToken.None);

        Assert.Equal("iPhone", result.Name);
        Assert.Equal("Apple", result.Brand);
    }

    [Fact]
    public async Task Should_throw_when_device_not_found()
    {
        _repositoryMock
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Device.Domain.Entities.Device?)null);

        await Assert.ThrowsAsync<DeviceNotFoundException>(() =>
            _service.GetByIdAsync(1, CancellationToken.None));
    }
}
