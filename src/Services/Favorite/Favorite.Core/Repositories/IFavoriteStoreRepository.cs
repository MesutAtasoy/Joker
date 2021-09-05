using System.Collections.Generic;
using System.Threading.Tasks;
using Favorite.Core.Entities;

namespace Favorite.Core.Repositories
{
    public interface IFavoriteStoreRepository
    {
        Task AddFavoriteStoreAsync(FavoriteStore store);
        Task<List<FavoriteStore>> GetStoresByStoreIdAsync(string storeId);
        Task<List<FavoriteStore>> GetStoresByUserIdAsync(string userId);
    }
}