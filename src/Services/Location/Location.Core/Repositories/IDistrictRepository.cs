using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.Repositories;
using Location.Core.Entities;

namespace Location.Core.Repositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<List<District>> ByCityIdAsync(Guid cityId);
    }
}