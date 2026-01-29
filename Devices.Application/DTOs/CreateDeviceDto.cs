using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.DTOs
{
    public class CreateDeviceDto
    {
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string State { get; set; } = null!;
    }
}
