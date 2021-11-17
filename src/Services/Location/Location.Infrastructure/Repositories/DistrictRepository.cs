using Joker.EntityFrameworkCore.Repositories;
using Location.Core.Entities;
using Location.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Location.Infrastructure.Repositories;

public class DistrictRepository : EntityFrameworkCoreRepository<LocationContext, District>, IDistrictRepository
{
    public DistrictRepository(LocationContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<District>> ByCityIdAsync(Guid cityId)
    {
        return await base.Get(x => !x.IsDeleted && x.CityId == cityId)
            .ToListAsync();
    }
}