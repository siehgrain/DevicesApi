using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.DTOs
{
    public class DeviceResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string State { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
