using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class BusinessDirectoryFeatureRepository : EntityFrameworkCoreRepository<ManagementContext, BusinessDirectoryFeature>,
        IBusinessDirectoryFeatureRepository
    {
        public BusinessDirectoryFeatureRepository(ManagementContext context)
            : base(context)
        {
        }
    }
}