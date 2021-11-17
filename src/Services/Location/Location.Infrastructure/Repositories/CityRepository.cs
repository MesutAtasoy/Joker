using Joker.EntityFrameworkCore.Repositories;
using Location.Core.Entities;
using Location.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Location.Infrastructure.Repositories;

public class CityRepository : EntityFrameworkCoreRepository<LocationContext,City>, ICityRepository
{
    public CityRepository(LocationContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<List<City>> ByCountryIdAsync(Guid countryId)
    {
        return await base.Get(x => !x.IsDeleted && x.CountryId == countryId)
            .ToListAsync();
    }
}