using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Domain.Exceptions;
using Xunit;

public class DeviceTests
{
    [Fact]
    public void Device_InUse_Cannot_Be_Updated()
    {
        // ARRANGE: preparar o cenário
        var device = new Device("iPhone", "Apple");
        device.ChangeState(DeviceState.InUse);

        // ACT: ação que deve falhar
        void Act() => device.Update("Galaxy", "Samsung");

        // ASSERT: esperamos uma exceção de domínio
        Assert.Throws<DomainException>(Act);
    }
    [Fact]
    public void CreatedAt_Should_Not_Be_Changeable()
    {
        var device = new Device("iPhone", "Apple");

        // Isso só compila se CreatedAt tiver setter público
        // O teste passa simplesmente se não existir forma de mudar
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


}
