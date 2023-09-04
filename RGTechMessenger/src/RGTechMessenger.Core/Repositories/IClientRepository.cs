using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task DeactivateAsync(Client client, CancellationToken cancellationToken = default);

        Task DeactivateManyAsync(Expression<Func<Client, bool>> predicate, CancellationToken cancellationToken = default);

        Task DeactivateAllAsync(CancellationToken cancellationToken = default);
    }
}
