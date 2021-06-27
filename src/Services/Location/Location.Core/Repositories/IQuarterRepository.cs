using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.Repositories;
using Location.Core.Entities;

namespace Location.Core.Repositories
{
    public interface IQuarterRepository : IRepository<Quarter>
    {
        Task<bool> ValidateAsync(Guid countryId,
            Guid cityId,
            Guid districtId,
            Guid neighborhoodId,
            Guid quarterId);

        Task<List<Quarter>> ByNeighborhoodIdAsync(Guid neighborhoodId);
    }
}