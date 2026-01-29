using Devices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.DTOs
{
    public class UpdateDeviceDto
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public DeviceState? State { get; set; }
    }
}
