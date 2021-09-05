using System;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Management.Buckets;
using Couchbase.Management.Collections;
using Microsoft.Extensions.Configuration;

namespace Favorite.Infrastructure.Initializers
{
    public class BucketInitializers : IBucketInitializers
    {
        private readonly IConfiguration _configuration;

        public BucketInitializers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
          
            var bucketName = _configuration.GetValue<string>("Couchbase:BucketName");
            var username = _configuration.GetValue<string>("Couchbase:UserName");
            var password = _configuration.GetValue<string>("Couchbase:Password");
            var connectionString = _configuration.GetValue<string>("Couchbase:ConnectionString");

            try
            {
                var cluster = await Cluster.ConnectAsync(connectionString, username, password);
                var buckets = await cluster.Buckets.GetAllBucketsAsync();

                var existedBucket  = buckets.ContainsKey(bucketName);

                if (existedBucket)
                {
                    return;
                }
            
                await cluster.Buckets.CreateBucketAsync(new BucketSettings
                {
                    Name = bucketName,
                    BucketType = BucketType.Couchbase,
                    RamQuotaMB = 2048
                });
                
                var bucket = await cluster.BucketAsync(bucketName);
                await CreateCollectionsAsync(bucket);
                await CreateIndexesAsync(cluster);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task CreateCollectionsAsync(IBucket bucket)
        {
            var scope = await bucket.DefaultScopeAsync();
            await bucket.Collections.CreateCollectionAsync(new CollectionSpec(scope.Name, "campaign"));
            await bucket.Collections.CreateCollectionAsync(new CollectionSpec(scope.Name, "store"));
        }
        
        private async Task CreateIndexesAsync(ICluster cluster)
        {
            await cluster.QueryAsync<dynamic>("CREATE PRIMARY INDEX ON `default`:`favorite`.`_default`.`campaign`");
            await cluster.QueryAsync<dynamic>("CREATE PRIMARY INDEX ON `default`:`favorite`.`_default`.`store`");
        }
    }
}