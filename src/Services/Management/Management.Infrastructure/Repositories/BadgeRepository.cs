using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class BadgeRepository : EntityFrameworkCoreRepository<ManagementContext,Badge>,
        IBadgeRepository
    {
        public BadgeRepository(ManagementContext context)
            : base(context)
        {
        }
    }
}