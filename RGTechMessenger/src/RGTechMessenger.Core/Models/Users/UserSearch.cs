using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Models.Users
{
    public class UserSearch
    {
        public long[]? Ids { get; set; }

        public bool? Online { get; set; }

        public Expression<Func<User, bool>> Build()
        {
            var predicate = PredicateBuilder.True<User>();

            if (Ids != null && Ids.Any())
            {
                predicate = predicate.And(user => Ids.Contains(user.Id));
            }

            if (Online.HasValue)
            {
                if (Online.Value)
                    predicate = predicate.And(user => user.Clients.Any(_ => _.Active));
                else
                    predicate = predicate.And(user => !user.Clients.Any(_ => _.Active));
            }

            return predicate;
        }
    }
}
