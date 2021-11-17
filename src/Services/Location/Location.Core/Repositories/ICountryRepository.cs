using Joker.Repositories;
using Location.Core.Entities;

namespace Location.Core.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetDefaultCountryAsync();
    }
}