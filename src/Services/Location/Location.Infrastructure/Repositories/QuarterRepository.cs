using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.EntityFrameworkCore.Repositories;
using Location.Core.Entities;
using Location.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Location.Infrastructure.Repositories
{
    public class QuarterRepository : EntityFrameworkCoreRepository<LocationContext, Quarter>, IQuarterRepository
    {
        public QuarterRepository(LocationContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Quarter>> ByNeighborhoodIdAsync(Guid neighborhoodId)
        {
            return await base.Get(x => !x.IsDeleted && x.NeighborhoodId == neighborhoodId)
                .ToListAsync();
        }
    }
}