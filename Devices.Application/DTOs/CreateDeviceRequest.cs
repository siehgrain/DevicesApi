using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.DTOs;

public class CreateDeviceRequest
{
    public string Name { get; set; } = default!;
    public string Brand { get; set; } = default!;
}

