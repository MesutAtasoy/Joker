using Joker.Repositories;
using Location.Core.Entities;

namespace Location.Core.Repositories
{
    public interface IQuarterRepository : IRepository<Quarter>
    {
        Task<List<Quarter>> ByNeighborhoodIdAsync(Guid neighborhoodId);
    }
}