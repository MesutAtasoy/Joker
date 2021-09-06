using System.Threading.Tasks;
using Aggregator.Api.Models.Favorite;
using Aggregator.Api.Models.Shared;
using Favorite.Api.Grpc;
using Grpc.Core;
using Joker.Extensions;
using Joker.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.Api.Services.Favorite
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly FavoriteApiGrpcService.FavoriteApiGrpcServiceClient _favoriteApiGrpcServiceClient;
        
        public FavoriteService(IHttpContextAccessor contextAccessor, 
            FavoriteApiGrpcService.FavoriteApiGrpcServiceClient favoriteApiGrpcServiceClient)
        {
            _contextAccessor = contextAccessor;
            _favoriteApiGrpcServiceClient = favoriteApiGrpcServiceClient;
        }
        public async Task<JokerBaseResponse<FavoriteCampaignModel>> AddFavoriteCampaignAsync(AddCampaignModel model)
        {
            var headers = await GetHeaders();

            var response = await _favoriteApiGrpcServiceClient.AddFavoriteCampaignAsync(new CreateFavoriteCampaignMessage
            {
                Campaign = new IdNameMessage
                {
                    Id = model?.Campaign?.Id.ToString() ?? "",
                    Name = model?.Campaign?.Name ?? ""
                },
            }, headers);
            
            if (response.Status != 200)
            {
                return new JokerBaseResponse<FavoriteCampaignModel>(null, response.Status, response.Message);
            }
            
            var favoriteCampaign = response.Data.Unpack<FavoriteCampaignMessage>();
            return new JokerBaseResponse<FavoriteCampaignModel>(As(favoriteCampaign), 200);
        }

        public async Task<JokerBaseResponse<FavoriteStoreModel>> AddFavoriteStoreAsync(AddStoreModel model)
        {
            var headers = await GetHeaders();

            var response = await _favoriteApiGrpcServiceClient.AddFavoriteStoreAsync(new CreateFavoriteStoreMessage
            {
                Store = new IdNameMessage
                {
                    Id = model?.Store?.Id.ToString() ?? "",
                    Name = model?.Store?.Name ?? ""
                }
            }, headers);
            
            if (response.Status != 200)
            {
                return new JokerBaseResponse<FavoriteStoreModel>(null, response.Status, response.Message);
            }
            
            var favoriteStore = response.Data.Unpack<FavoriteStoreMessage>();
            return new JokerBaseResponse<FavoriteStoreModel>(As(favoriteStore), 200);
        }

        private async Task<Metadata> GetHeaders()
        {
            var accessToken = await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var headers = new Metadata();
            if (!string.IsNullOrEmpty(accessToken))
            {
                headers.Add("Authorization", $"Bearer {accessToken}");
            }

            return headers;
        }

        private FavoriteCampaignModel As(FavoriteCampaignMessage message)
        {
            return new FavoriteCampaignModel
            {
                Campaign = new Models.Shared.IdNameModel
                {
                    Id = message.Campaign.Id.ToGuid(),
                    Name = message.Campaign.Name
                },
                UserInfo = new UserModel
                {
                    Id = message.User.Id,
                    Username = message.User.UserName
                },
                CreatedDate = message.CreatedDate.ToDateTime()
            };
        }
        
        private FavoriteStoreModel As(FavoriteStoreMessage message)
        {
            return new FavoriteStoreModel
            {
                Store = new Models.Shared.IdNameModel
                {
                    Id = message.Store.Id.ToGuid(),
                    Name = message.Store.Name
                },
                UserInfo = new UserModel
                {
                    Id = message.User.Id,
                    Username = message.User.UserName
                },
                CreatedDate = message.CreatedDate.ToDateTime()
            };
        }
    }
}