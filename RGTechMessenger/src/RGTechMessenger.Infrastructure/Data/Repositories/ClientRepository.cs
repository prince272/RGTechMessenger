﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Events.Clients;
using RGTechMessenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RGTechMessenger.Infrastructure.Data.Repositories
{
    public class ClientRepository : AppRepository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task DeactivateAllAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<Client>().ExecuteUpdateAsync(calls => calls.SetProperty(client => client.Active, client => false), cancellationToken);
        }

        public Task DeactivateAsync(Client client, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            client.Active = false;
            _dbContext.Update(client);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeactivateManyAsync(Expression<Func<Client, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return _dbContext.Set<Client>().Where(predicate).ExecuteUpdateAsync(calls => calls.SetProperty(client => client.Active, client => false), cancellationToken);
        }
    }
}