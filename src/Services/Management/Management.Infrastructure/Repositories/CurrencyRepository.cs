using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;

namespace Management.Infrastructure.Repositories;

public class CurrencyRepository : EntityFrameworkCoreRepository<ManagementContext, Currency>, ICurrencyRepository
{
    public CurrencyRepository(ManagementContext context)
        : base(context)
    {
    }

    public async Task<Currency> GetDefaultCurrencyAsync()
    {
        return await FirstOrDefaultAsync();
    }
}