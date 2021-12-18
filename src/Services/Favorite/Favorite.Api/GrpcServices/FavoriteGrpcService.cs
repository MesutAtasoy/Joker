using Favorite.Api.Grpc;
using Favorite.Application.Campaigns;
using Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign;
using Favorite.Application.Stores;
using Favorite.Application.Stores.Commands.CreateFavoriteStore;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Favorite.Api.GrpcServices;

[Authorize]
public class FavoriteGrpcService : FavoriteApiGrpcService.FavoriteApiGrpcServiceBase
{
    private readonly FavoriteCampaignManager _campaignManager;
    private readonly FavoriteStoreManager _storeManager;
        
    public FavoriteGrpcService(FavoriteCampaignManager campaignManager, 
        FavoriteStoreManager storeManager)
    {
        _campaignManager = campaignManager;
        _storeManager = storeManager;
    }
        
    public override async Task<CampaignBaseGrpcResponse> AddFavoriteStore(CreateFavoriteStoreMessage request, ServerCallContext context)
    {
        var response = await _storeManager.AddFavoriteStoreAsync(new CreateFavoriteStoreCommand
        {
            Id = request.Store.Id.ToGuid(),
            Name = request.Store.Name,
            OrganizationId = request.Store.OrganizationId.ToGuid()
        });

        var favoriteCampaignMessage = new FavoriteStoreMessage
        {
            Store = new FavoriteStoreItemMessage
            {
                Id = response?.Store?.Id.ToString() ?? " ",
                Name = response?.Store?.Name ?? " ",
            },
            User = new UserMessage
            {
                Id = response?.UserInfo?.Id ?? " ",
                UserName = response?.UserInfo?.Username ?? " "
            },
            CreatedDate = response?.CreatedDate.ToTimestamp()
        };
            
        return new CampaignBaseGrpcResponse
        {
            Data = Any.Pack(favoriteCampaignMessage),
            Message = " ",
            Status = 200
        };
    }

    public override async Task<CampaignBaseGrpcResponse> AddFavoriteCampaign(CreateFavoriteCampaignMessage request, ServerCallContext context)
    {
        var response = await _campaignManager.AddFavoriteCampaignAsync(new CreateFavoriteCampaignCommand 
        {
            Id = request.Campaign.Id.ToGuid(),
            Title = request.Campaign.Title,
            Slug = request.Campaign.Slug,
            SlugKey = request.Campaign.SlugKey,
            OrganizationId = request.Campaign.OrganizationId.ToGuid()
        });

        var favoriteCampaignMessage = new FavoriteCampaignMessage
        {
            Campaign = new FavoriteCampaignItemMessage
            {
                Id = response?.Campaign?.Id.ToString() ?? " ",
                Title = response?.Campaign?.Title ?? " ",
                Slug = response?.Campaign?.Slug ?? " ",
                SlugKey = response?.Campaign?.SlugKey ?? ""
            },
            User = new UserMessage
            {
                Id = response?.UserInfo?.Id ?? " ",
                UserName = response?.UserInfo?.Username ?? " "
            },
            CreatedDate = response?.CreatedDate.ToTimestamp()
        };
            
        return new CampaignBaseGrpcResponse
        {
            Data = Any.Pack(favoriteCampaignMessage),
            Message = " ",
            Status = 200
        };
    }

    public override async Task<CampaignBaseGrpcResponse> GetFavoriteStoresByUserId(ByIdMessage request, ServerCallContext context)
    {
        var stores = await _storeManager.GetStoresByUserIdAsync(request.Id);

        var favoriteCampaignMessages = stores.Select(x =>  new FavoriteStoreMessage
        {
            Store = new FavoriteStoreItemMessage
            {
                Id = x?.Store?.Id.ToString() ?? " ",
                Name = x?.Store?.Name ?? " ",
            },
            User = new UserMessage
            {
                Id = x?.UserInfo?.Id ?? " ",
                UserName = x?.UserInfo?.Username ?? " "
            },
            CreatedDate = x?.CreatedDate.ToTimestamp()
        }).ToList();

        var favoriteStoreMessageList = new FavoriteStoreMessageList
        {
            Stores = { favoriteCampaignMessages }
        };
            
        return new CampaignBaseGrpcResponse
        {
            Data = Any.Pack(favoriteStoreMessageList),
            Message = " ",
            Status = 200
        };

    }

    public override async Task<CampaignBaseGrpcResponse> GetFavoriteCampaignsByCampaignId(ByIdMessage request, ServerCallContext context)
    {
        var campaigns = await _campaignManager.GetCampaignsByCampaignIdAsync(request.Id);

        var favoriteCampaignMessages = campaigns.Select(x =>  new FavoriteCampaignMessage
        {
            Campaign = new FavoriteCampaignItemMessage
            {
                Id = x?.Campaign?.Id.ToString() ?? " ",
                Title = x?.Campaign?.Title ?? " ",
                Slug = x?.Campaign?.Slug ?? " ",
                SlugKey = x?.Campaign?.SlugKey ?? ""
            },
            User = new UserMessage
            {
                Id = x?.UserInfo?.Id ?? " ",
                UserName = x?.UserInfo?.Username ?? " "
            },
            CreatedDate = x?.CreatedDate.ToTimestamp()
        }).ToList();

        var favoriteCampaignMessageList = new FavoriteCampaignMessageList()
        {
            Campaigns = { favoriteCampaignMessages }
        };
            
        return new CampaignBaseGrpcResponse
        {
            Data = Any.Pack(favoriteCampaignMessageList),
            Message = " ",
            Status = 200
        };        
    }

    public override async Task<CampaignBaseGrpcResponse> GetFavoriteStoresByStoreId(ByIdMessage request, ServerCallContext context)
    {
        var stores = await _storeManager.GetStoresByStoreIdAsync(request.Id);

        var favoriteCampaignMessages = stores.Select(x =>  new FavoriteStoreMessage
        {
            Store = new FavoriteStoreItemMessage
            {
                Id = x?.Store?.Id.ToString() ?? " ",
                Name = x?.Store?.Name ?? " ",
            },
            User = new UserMessage
            {
                Id = x?.UserInfo?.Id ?? " ",
                UserName = x?.UserInfo?.Username ?? " "
            },
            CreatedDate = x?.CreatedDate.ToTimestamp()
        }).ToList();

        var favoriteStoreMessageList = new FavoriteStoreMessageList
        {
            Stores = { favoriteCampaignMessages }
        };
            
        return new CampaignBaseGrpcResponse
        {
            Data = Any.Pack(favoriteStoreMessageList),
            Message = " ",
            Status = 200
        };        
    }

    public override async Task<CampaignBaseGrpcResponse> GetFavoriteCampaignsByUserId(ByIdMessage request, ServerCallContext context)
    {
        var campaigns = await _campaignManager.GetCampaignsByUserIdAsync(request.Id);

        var favoriteCampaignMessages = campaigns.Select(x =>  new FavoriteCampaignMessage
        {
            Campaign = new FavoriteCampaignItemMessage
            {
                Id = x?.Campaign?.Id.ToString() ?? " ",
                Title = x?.Campaign?.Title ?? " ",
                Slug = x?.Campaign?.Slug ?? " ",
                SlugKey = x?.Campaign?.SlugKey ?? ""
            },
            User = new UserMessage
            {
                Id = x?.UserInfo?.Id ?? " ",
                UserName = x?.UserInfo?.Username ?? " "
            },
            CreatedDate = x?.CreatedDate.ToTimestamp()
        }).ToList();

        var favoriteCampaignMessageList = new FavoriteCampaignMessageList()
        {
            Campaigns = { favoriteCampaignMessages }
        };
            
        return new CampaignBaseGrpcResponse
        {
            Data = Any.Pack(favoriteCampaignMessageList),
            Message = " ",
            Status = 200
        };        
    }
}