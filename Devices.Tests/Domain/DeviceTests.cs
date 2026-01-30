using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Domain.Exceptions;
using Xunit;

public class DeviceTests
{
    [Fact]
    public void Device_InUse_Cannot_Be_Updated()
    {
        var device = new Device("iPhone", "Apple");

        device.ChangeState(DeviceState.InUse);
        void Act() => device.Update("Galaxy", "Samsung");

        Assert.Throws<DomainException>(Act);
    }
    [Fact]
    public void CreatedAt_Should_Not_Be_Changeable()
    {
        var device = new Device("iPhone", "Apple");

        Assert.True(device.CreatedAt <= DateTime.UtcNow);
    }
    [Fact]
    public void Device_Available_Can_Be_Updated()
    {
        var device = new Device("iPhone", "Apple");

        device.Update("Galaxy", "Samsung");

        Assert.Equal("Galaxy", device.Name);
        Assert.Equal("Samsung", device.Brand);
    }
    [Fact]
    public void Invalid_DeviceState_Should_Throw_Exception()
    {
        var device = new Device("iPhone", "Apple");

        void Act() => device.ChangeState((DeviceState)999);

        Assert.Throws<DomainException>(Act);
    }
    [Fact]
    public void Device_Constructor_Should_Throw_If_Name_Empty()
    {
        Assert.Throws<DomainException>(() => new Device("", "Apple"));
    }

    [Fact]
    public void Device_Constructor_Should_Throw_If_Brand_Empty()
    {
        Assert.Throws<DomainException>(() => new Device("iPhone", "  "));
    }
}
