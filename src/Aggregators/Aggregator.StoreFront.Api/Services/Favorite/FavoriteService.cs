using Aggregator.StoreFront.Api.Models.Favorite;
using Aggregator.StoreFront.Api.Models.Favorite.Requests;
using Aggregator.StoreFront.Api.Services.BaseGrpc;
using AutoMapper;
using Favorite.Api.Grpc;
using Joker.Response;

namespace Aggregator.StoreFront.Api.Services.Favorite;

public class FavoriteService : IFavoriteService
{
    private readonly FavoriteApiGrpcService.FavoriteApiGrpcServiceClient _favoriteApiGrpcServiceClient;
    private readonly IBaseGrpcProvider _grpcProvider;
    private readonly IMapper _mapper;
    
    public FavoriteService(FavoriteApiGrpcService.FavoriteApiGrpcServiceClient favoriteApiGrpcServiceClient,
        IBaseGrpcProvider grpcProvider,
        IMapper mapper)
    {
        _favoriteApiGrpcServiceClient = favoriteApiGrpcServiceClient;
        _grpcProvider = grpcProvider;
        _mapper = mapper;
    }
    public async Task<JokerBaseResponse<FavoriteCampaignModel>> AddFavoriteCampaignAsync(AddFavoriteCampaignRequestModel requestModel)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var request = _mapper.Map<FavoriteCampaignItemMessage>(requestModel);

        var response = await _favoriteApiGrpcServiceClient.AddFavoriteCampaignAsync(new CreateFavoriteCampaignMessage
        {
            Campaign = request
        }, headers);
            
        if (response.Status != 200)
        {
            return new JokerBaseResponse<FavoriteCampaignModel>(null, response.Status, response.Message);
        }
            
        var favoriteCampaign = response.Data.Unpack<FavoriteCampaignMessage>();
        var favoriteCampaignModel = _mapper.Map<FavoriteCampaignModel>(favoriteCampaign);
        return new JokerBaseResponse<FavoriteCampaignModel>(favoriteCampaignModel, 200);
    }

    public async Task<JokerBaseResponse<FavoriteStoreModel>> AddFavoriteStoreAsync(AddFavoriteStoreRequestModel requestModel)
    {
        var headers = await _grpcProvider.GetDefaultHeadersAsync();

        var request = _mapper.Map<FavoriteStoreItemMessage>(requestModel);
        
        var response = await _favoriteApiGrpcServiceClient.AddFavoriteStoreAsync(new CreateFavoriteStoreMessage
        {
            Store = request
        }, headers);
            
        if (response.Status != 200)
        {
            return new JokerBaseResponse<FavoriteStoreModel>(null, response.Status, response.Message);
        }
            
        var favoriteStore = response.Data.Unpack<FavoriteStoreMessage>();
        var favoriteStoreModel = _mapper.Map<FavoriteStoreModel>(favoriteStore);
        return new JokerBaseResponse<FavoriteStoreModel>(favoriteStoreModel, 200);
    }
}