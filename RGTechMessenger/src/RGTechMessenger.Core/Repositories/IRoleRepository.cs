using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
