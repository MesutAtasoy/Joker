using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.ElasticSearch.Exceptions;
using Joker.ElasticSearch.Models;
using Joker.ElasticSearch.Service;
using Nest;

namespace Search.Core.IndexManagers
{
    public class ElasticIndexManager : IElasticSearchManager
    {
         private readonly IElasticClient _elasticClient;

        public ElasticIndexManager(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task CreateIndexAsync<T, TKey>(string indexName) where T : ElasticEntity<TKey>
        {
            var indexExistsResponse = await _elasticClient.Indices.ExistsAsync(indexName);

            if (indexExistsResponse.Exists)
                return;
            
            var result = await _elasticClient.Indices
                .CreateAsync(indexName, ss => ss.Index(indexName)
                            .Settings(
                                o => o.NumberOfShards(1)
                                    .NumberOfReplicas(0)
                                    .Setting("max_result_window", int.MaxValue)
                                    .Analysis(a => 
                                        a.TokenFilters(tkf => tkf.AsciiFolding("my_ascii_folding",
                                                af => af.PreserveOriginal(true)))
                                        .Analyzers(aa => aa.Custom("turkish_analyzer", 
                                            ca => ca.Filters("lowercase", "my_ascii_folding")
                                                .Tokenizer("standard"))))
                            )
                            .Mappings(m => m.Map<T>(mm => mm.AutoMap()
                                .Properties(p => p.Text(t => t.Name(n => n.SearchingArea).Analyzer("turkish_analyzer")
                                )))));
            
            if (!result.Acknowledged)
                throw new ElasticSearchException($"Create Index {indexName} failed : :" +  result.ServerError.Error.Reason);
            
            await _elasticClient.Indices.BulkAliasAsync(al => al.Add(add => add.Index(indexName).Alias(indexName)));

        }
        public async Task DeleteIndexAsync(string indexName)
        {
            var response = await _elasticClient.Indices.DeleteAsync(indexName);
            if (response.Acknowledged) 
                return;
            
            throw new ElasticSearchException($"Delete index {indexName} failed :{response.ServerError.Error.Reason}");
        }
        public async Task AddOrUpdateAsync<T, TKey>(string indexName, T model) where T : ElasticEntity<TKey>
        {
            var documentExists = _elasticClient.DocumentExists(DocumentPath<T>.Id(new Id(model)), dd => dd.Index(indexName));

            if (documentExists.Exists)
            {
                var result = await _elasticClient.UpdateAsync(DocumentPath<T>.Id(new Id(model)),
                    ss => ss.Index(indexName).Doc(model).RetryOnConflict(3));

                if (result.ServerError == null) 
                    return;
                
                throw new ElasticSearchException($"Update Document failed at index{indexName} :" + result.ServerError.Error.Reason);
            }
            else
            {
                var result = await _elasticClient.IndexAsync(model, ss => ss.Index(indexName));
                if (result.ServerError == null) return;
                throw new ElasticSearchException($"Insert Document failed at index {indexName} :" + result.ServerError.Error.Reason);
            }
        }
        public async Task DeleteAsync<T, TKey>(string indexName, string typeName, T model) where T : ElasticEntity<TKey>
        {
            var response = await _elasticClient.DeleteAsync(new DeleteRequest(indexName,model.Id.ToString()));
            if (response.ServerError == null) 
                return;
            
            throw new ElasticSearchException($"Delete Document at index {indexName} :{response.ServerError.Error.Reason}");
        }
        public async Task ReIndex<T, TKey>(string indexName) where T : ElasticEntity<TKey>
        {
            await DeleteIndexAsync(indexName);
            await CreateIndexAsync<T, TKey>(indexName);
        }
        public async Task CrateIndexAsync(string indexName)
        {
            var indexExists = await _elasticClient.Indices.ExistsAsync(indexName);
            if (indexExists.Exists)
                return;
            
            var result = await _elasticClient.Indices.CreateAsync(indexName, ss =>
                        ss.Index(indexName)
                            .Settings(o => o.NumberOfShards(1).NumberOfReplicas(0).Setting("max_result_window", int.MaxValue)));
            
            if (!result.Acknowledged)
                throw new ElasticSearchException($"Create Index {indexName} failed :" + result.ServerError.Error.Reason);
            
            await _elasticClient.Indices.BulkAliasAsync(al => al.Add(add => add.Index(indexName).Alias(indexName)));
        }
        public async Task BulkAddOrUpdateAsync<T, TKey>(string indexName, List<T> list, int bulkNum = 1000)
            where T : ElasticEntity<TKey>
        {
            var bulk = new BulkRequest(indexName)
            {
                Operations = new List<IBulkOperation>()
            };
            foreach (var item in list)
            {
                bulk.Operations.Add(new BulkIndexOperation<T>(item));
            }
            var response = await _elasticClient.BulkAsync(bulk);
            if (response.Errors)
                throw new ElasticSearchException($"Bulk InsertOrUpdate Document failed at index {indexName} :{response.ServerError.Error.Reason}");
        }
        public async Task BulkDeleteAsync<T, TKey>(string indexName, List<T> list, int bulkNum = 1000)
            where T : ElasticEntity<TKey>
        {
            var bulk = new BulkRequest(indexName)
            {
                Operations = new List<IBulkOperation>()
            };
            foreach (var item in list)
            {
                bulk.Operations.Add(new BulkDeleteOperation<T>(new Id(item)));
            }
            var response = await _elasticClient.BulkAsync(bulk);
            if (response.Errors)
                throw new ElasticSearchException($"Bulk Delete Document at index {indexName} :{response.ServerError.Error.Reason}");
        }
    }
}