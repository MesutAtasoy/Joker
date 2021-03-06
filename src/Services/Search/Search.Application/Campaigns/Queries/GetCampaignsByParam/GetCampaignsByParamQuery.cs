using MediatR;
using Search.Application.Shared.Dto.Request;
using Search.Core.IndexModels;

namespace Search.Application.Campaigns.Queries.GetCampaignsByParam;

public class GetCampaignsByParamQuery : SearchBaseRequest, IRequest<SearchBaseResponse<CampaignIndexModel>>
{
    public string StoreId { get; set; }
    public string StoreName  { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public string PreviewImageUrl { get; set; }
}