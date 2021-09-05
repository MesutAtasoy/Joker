using System.Collections.Generic;
using System.Threading.Tasks;
using Favorite.Core.Entities;

namespace Favorite.Core.Repositories
{
    public interface IFavoriteCampaignRepository
    {
        Task AddFavoriteCampaignAsync(FavoriteCampaign campaign);
        Task<List<FavoriteCampaign>> GetCampaignsByCampaignIdAsync(string campaignId);
        Task<List<FavoriteCampaign>> GetCampaignsByUserIdAsync(string userId);
    }
}