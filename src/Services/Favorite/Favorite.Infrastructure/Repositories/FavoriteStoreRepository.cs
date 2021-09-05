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
    public class FavoriteStoreRepository : IFavoriteStoreRepository
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly string _bucketName;
        
        public FavoriteStoreRepository(IBucketProvider bucketProvider,
            IConfiguration configuration)
        {
            _bucketProvider = bucketProvider;
            _bucketName = configuration.GetValue<string>("Couchbase:BucketName");
        }
        
        public async Task AddFavoriteStoreAsync(FavoriteStore store)
        {
            var bucket = await _bucketProvider.GetBucketAsync(_bucketName);
            
            var collection = await bucket.CollectionAsync("store");
            
            await collection.InsertAsync(Guid.NewGuid().ToString(), store);
        }

        public async Task<List<FavoriteStore>> GetStoresByStoreIdAsync(string storeId)
        {
            var bucket = await _bucketProvider.GetBucketAsync(_bucketName);

            var scope = await bucket.DefaultScopeAsync();

            var favoriteStores = await scope.QueryAsync<FavoriteStore>($"SELECT c.* FROM  favorite._default.store c " +
                                                                             $"WHERE c.store.id =\"{storeId}\"");

            return await favoriteStores.Rows.ToListAsync();
        }
        
        public async Task<List<FavoriteStore>> GetStoresByUserIdAsync(string userId)
        {
            var bucket = await _bucketProvider.GetBucketAsync(_bucketName);

            var scope = await bucket.DefaultScopeAsync();

            var favoriteStores = await scope.QueryAsync<FavoriteStore>($"SELECT c.* FROM  favorite._default.store c " +
                                                                             $"WHERE c.userinfo.id =\"{userId}\"");

            return await favoriteStores.Rows.ToListAsync();
        }
    }
}