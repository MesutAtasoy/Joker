using Joker.Repositories;
using Location.Core.Entities;

namespace Location.Core.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<List<City>> ByCountryIdAsync(Guid countryId);
    }
}