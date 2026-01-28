using Devices.Domain.Enums;
using Devices.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Brand { get; private set; }
        public DeviceState State { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Device(string name, string brand)
        {
            Id = Guid.NewGuid();
            Name = name;
            Brand = brand;
            State = DeviceState.Available;
            CreatedAt = DateTime.UtcNow;
        }
        public void Update(string name, string brand)
        {
            if (State == DeviceState.InUse)
                throw new DomainException("Device in use cannot be updated");

            Name = name;
            Brand = brand;
        }

        public void ChangeState(DeviceState newState)
        {
            State = newState;
        }

        public void EnsureCanBeDeleted()
        {
            if (State == DeviceState.InUse)
                throw new DomainException("Device in use cannot be deleted");
        }

    }
}
