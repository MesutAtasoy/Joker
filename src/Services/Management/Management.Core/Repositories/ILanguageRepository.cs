using Joker.Repositories;
using Management.Core.Entities;

namespace Management.Core.Repositories;

public interface ILanguageRepository : IRepository<Language>
{
    Task<Language> GetDefaultLanguageAsync();
}