using System.Text.Json;
using Aggregator.StoreFront.Api.Models.Favorite;
using Campaign.Api.Grpc;
using Grpc.Core;
using Joker.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.StoreFront.Api.Services.Campaign;

public class CampaignService : ICampaignService
{
    private readonly CampaignApiGrpcService.CampaignApiGrpcServiceClient _campaignApiGrpcServiceClient;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<CampaignService> _logger;
    public CampaignService(CampaignApiGrpcService.CampaignApiGrpcServiceClient campaignApiGrpcServiceClient,
        IHttpContextAccessor contextAccessor, ILogger<CampaignService> logger)
    {
        _campaignApiGrpcServiceClient = campaignApiGrpcServiceClient;
        _contextAccessor = contextAccessor;
        _logger = logger;
    }
    
    public async Task<Models.Campaign.CampaignModel> GetByIdAsync(Guid id)
    {
        var headers = await GetHeaders();

        var campaign = await _campaignApiGrpcServiceClient.GetByIdAsync(new ByIdMessage { Id = id.ToString() }, headers);
        _logger.LogError(JsonSerializer.Serialize(campaign));
        return As(campaign);
    }

    private async Task<Metadata> GetHeaders()
    {
        var accessToken =
            await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var headers = new Metadata();
        if (!string.IsNullOrEmpty(accessToken))
        {
            headers.Add("Authorization", $"Bearer {accessToken}");
        }

        return headers;
    }

    #region Converters

    private Models.Campaign.CampaignModel As(CampaignMessage campaignMessage)
    {
        return new ()
        {
            Id = campaignMessage.Id.ToGuid(),
            Title = campaignMessage.Title,
            Condition = campaignMessage.Condition,
            Channel = campaignMessage.Channel,
            Code = campaignMessage.Code,
            Description = campaignMessage.Description,
            BusinessDirectory = new Models.Shared.IdNameModel
            {
                Id = campaignMessage.BusinessDirectory?.Id.ToGuid() ?? Guid.Empty,
                Name = campaignMessage.BusinessDirectory?.Name
            },
            Store = new Models.Shared.IdNameModel()
            {
                Id = campaignMessage.Store?.Id?.ToGuid() ?? Guid.Empty,
                Name = campaignMessage.Store?.Name
            },
            Merchant = new Models.Shared.IdNameModel
            {
                Id = campaignMessage.Merchant?.Id?.ToGuid() ?? Guid.Empty,
                Name = campaignMessage.Merchant?.Name
            },
            EndTime = campaignMessage.EndTime?.ToDateTime(),
            StartTime = campaignMessage.StartTime?.ToDateTime(),
            PreviewImageUrl = campaignMessage.PreviewImageUrl,
            CreatedDate = campaignMessage.CreatedDate.ToDateTime(),
            ModifiedDate = campaignMessage.ModifiedDate?.ToDateTime(),
            Slug = campaignMessage.Slug,
            SlugKey = campaignMessage.SlugKey
        };
    }

    #endregion
}