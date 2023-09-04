using Microsoft.AspNetCore.Identity;
using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Entities
{
    public class Role : IdentityRole<long>, IEntity
    {
        public Role()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }

        public virtual ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
