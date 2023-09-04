using AbstractProfile = AutoMapper.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Extensions.Identity;

namespace RGTechMessenger.Core.Models.Users
{
    public class UserModel
    {
        public long Id { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public bool Active { get; set; }

        public bool Online { get; set; }

        public DateTimeOffset LastActiveAt { get; set; }

        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
    }

    public class UserProfile : AbstractProfile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
        }
    }
}
