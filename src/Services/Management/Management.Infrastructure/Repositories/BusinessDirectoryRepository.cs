using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class BusinessDirectoryRepository: EntityFrameworkCoreRepository<ManagementContext,BusinessDirectory>, 
        IBusinessDirectoryRepository
    {
        public BusinessDirectoryRepository(ManagementContext context) 
            : base(context)
        {
        }
    }
}