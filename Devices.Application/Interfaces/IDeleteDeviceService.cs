using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Interfaces
{
    public interface IDeleteDeviceService
    {
        Task<bool> ExecuteAsync(int id);
    }
}
