using Microsoft.AspNetCore.Identity;
using RGTechMessenger.Core.Entities;
using RGTechMessenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Infrastructure.Data.Repositories
{
    public class MediaRepository : AppRepository<Media>, IMediaRepository
    {
        public MediaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
