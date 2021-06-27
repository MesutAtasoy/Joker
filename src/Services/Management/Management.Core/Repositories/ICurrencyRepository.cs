using System.Threading.Tasks;
using Joker.Repositories;
using Management.Core.Entities;

namespace Management.Core.Repositories
{
    public interface ICurrencyRepository:  IRepository<Currency>
    {
        Task<Currency> GetDefaultCurrencyAsync();
    }
}