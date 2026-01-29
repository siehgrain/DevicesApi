using Devices.Api.Controllers;
using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class DevicesControllerTests
{
    private readonly Mock<ICreateDeviceService> _createService = new();
    private readonly Mock<IUpdateDeviceService> _updateService = new();
    private readonly Mock<IGetDevicesService> _getService = new();
    private readonly Mock<IDeleteDeviceService> _deleteService = new();

    private DevicesController CreateController()
        => new(
            _createService.Object,
            _updateService.Object,
            _getService.Object,
            _deleteService.Object
        );

    [Fact]
    public async Task Create_Should_Return_201()
    {
        // Arrange
        var controller = CreateController();

        var request = new CreateDeviceRequest
        {
            Name = "iPhone",
            Brand = "Apple"
        };

        var device = new Device("iPhone", "Apple");

        _createService
            .Setup(x => x.CreateAsync(It.IsAny<CreateDeviceRequest>()))
            .ReturnsAsync(device);

        // Act
        var result = await controller.Create(request);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Update_Should_Return_204_When_Updated()
    {
        _updateService
            .Setup(x => x.ExecuteAsync(1, It.IsAny<UpdateDeviceDto>()))
            .ReturnsAsync(true);

        var result = await CreateController()
            .Update(1, new UpdateDeviceDto());

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Update_Should_Return_404_When_NotFound()
    {
        _updateService
            .Setup(x => x.ExecuteAsync(1, It.IsAny<UpdateDeviceDto>()))
            .ReturnsAsync(false);

        var result = await CreateController()
            .Update(1, new UpdateDeviceDto());

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetById_Should_Return_404_When_NotFound()
    {
        _getService
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Device?)null);

        var result = await CreateController().GetById(1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAll_Should_Return_List()
    {
        _getService
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync([]);

        var result = await CreateController().GetAll();

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Delete_Should_Return_204_When_Deleted()
    {
        _deleteService
            .Setup(x => x.ExecuteAsync(1))
            .ReturnsAsync(true);

        var result = await CreateController().Delete(1);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_Should_Return_404_When_NotFound()
    {
        _deleteService
            .Setup(x => x.ExecuteAsync(1))
            .ReturnsAsync(false);

        var result = await CreateController().Delete(1);

        result.Should().BeOfType<NotFoundResult>();
    }
}
