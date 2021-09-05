using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Extensions.DependencyInjection;
using Favorite.Core.Entities;
using Favorite.Core.Repositories;
using Microsoft.Extensions.Configuration;

namespace Favorite.Infrastructure.Repositories
{
    public class FavoriteCampaignRepository : IFavoriteCampaignRepository
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly string _bucketName;
        
        public FavoriteCampaignRepository(IBucketProvider bucketProvider,
            IConfiguration configuration)
        {
            _bucketProvider = bucketProvider;
            _bucketName = configuration.GetValue<string>("Couchbase:BucketName");
        }
        
        public async Task AddFavoriteCampaignAsync(FavoriteCampaign campaign)
        {
            var bucket = await _bucketProvider.GetBucketAsync(_bucketName);
            
            var collection = await bucket.CollectionAsync("campaign");
            
            await collection.InsertAsync(Guid.NewGuid().ToString(), campaign);
        }

        public async Task<List<FavoriteCampaign>> GetCampaignsByCampaignIdAsync(string campaignId)
        {
            var bucket = await _bucketProvider.GetBucketAsync(_bucketName);

            var scope = await bucket.DefaultScopeAsync();

            var favoriteCampaigns = await scope.QueryAsync<FavoriteCampaign>($"SELECT c.* FROM  favorite._default.campaign c " +
                                                                $"WHERE c.campaign.id =\"{campaignId}\"");

            return await favoriteCampaigns.Rows.ToListAsync();
        }
        
        public async Task<List<FavoriteCampaign>> GetCampaignsByUserIdAsync(string userId)
        {
            var bucket = await _bucketProvider.GetBucketAsync(_bucketName);

            var scope = await bucket.DefaultScopeAsync();

            var favoriteCampaigns = await scope.QueryAsync<FavoriteCampaign>($"SELECT c.* FROM  favorite._default.campaign c " +
                                                                             $"WHERE c.userinfo.id =\"{userId}\"");

            return await favoriteCampaigns.Rows.ToListAsync();
        }
    }
}