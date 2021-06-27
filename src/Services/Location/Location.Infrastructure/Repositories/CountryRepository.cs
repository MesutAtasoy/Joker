using System.Threading.Tasks;
using Joker.EntityFrameworkCore.Repositories;
using Location.Core.Entities;
using Location.Core.Repositories;

namespace Location.Infrastructure.Repositories
{
    public class CountryRepository : EntityFrameworkCoreRepository<LocationContext, Country>, ICountryRepository
    {
        public CountryRepository(LocationContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Country> GetDefaultCountryAsync()
        {
            return await base.FirstOrDefaultAsync(x => !x.IsDeleted);
        }
    }
}