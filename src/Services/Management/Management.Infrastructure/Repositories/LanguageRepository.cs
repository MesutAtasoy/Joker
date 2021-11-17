using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;

namespace Management.Infrastructure.Repositories;

public class LanguageRepository : EntityFrameworkCoreRepository<ManagementContext,Language>, ILanguageRepository
{
    public LanguageRepository(ManagementContext context)
        : base(context)
    {
    }

    public async Task<Language> GetDefaultLanguageAsync()
    {
        return await FirstOrDefaultAsync();
    }
}