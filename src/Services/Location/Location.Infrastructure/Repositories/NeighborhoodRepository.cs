using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.EntityFrameworkCore.Repositories;
using Location.Core.Entities;
using Location.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Location.Infrastructure.Repositories
{
    public class NeighborhoodRepository : EntityFrameworkCoreRepository<LocationContext, Neighborhood>, INeighborhoodRepository
    {
        public NeighborhoodRepository(LocationContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Neighborhood>> ByDistrictIdAsync(Guid districtId)
        {
            return await base.Get(x => !x.IsDeleted && x.DistrictId == districtId)
                .ToListAsync();
        }
    }
}