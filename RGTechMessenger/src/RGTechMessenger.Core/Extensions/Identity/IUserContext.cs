using RGTechMessenger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Extensions.Identity
{
    public interface IUserContext
    {
        string? DeviceId { get; }

        string? IpAddress { get; }

        long? UserId { get; }

        string? UserAgent { get; }
    }
}
