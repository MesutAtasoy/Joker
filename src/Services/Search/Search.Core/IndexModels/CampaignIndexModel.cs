using Joker.ElasticSearch.Models;

namespace Search.Core.IndexModels;

public sealed class CampaignIndexModel : ElasticEntity<Guid>
{
    public Guid StoreId { get; set; }
    public string StoreName { get; set; }
    public Guid MerchantId { get; set; }
    public string MerchantName { get; set; }
    public Guid BusinessDirectoryId { get;  set; }
    public string BusinessDirectoryName { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public string PreviewImageUrl { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}