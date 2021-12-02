using Aggregator.StoreFront.Api.Models.Favorite.Requests;
using Aggregator.StoreFront.Api.Models.Shared;
using AutoMapper;
using Favorite.Api.Grpc;
using Joker.Extensions;

namespace Aggregator.StoreFront.Api.Models.Favorite.MappingProfiles;

public class FavoriteMappingProfile : Profile
{
    public FavoriteMappingProfile()
    {
        CreateMap<UserMessage, UserModel>();

        CreateMap<FavoriteCampaignItemMessage, FavoriteCampaignItemModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()));
        
        CreateMap<FavoriteStoreItemMessage, FavoriteStoreItemModel>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToGuid()));
        
        CreateMap<FavoriteStoreMessage, FavoriteStoreModel>()
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()));
        
        CreateMap<FavoriteCampaignMessage, FavoriteCampaignModel>()
            .ForMember(dest => dest.CreatedDate, src => src.MapFrom(m => m.CreatedDate.ToDateTime()));
        
        CreateMap<AddFavoriteStoreRequestModel, FavoriteStoreItemMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToString()));

        CreateMap<AddFavoriteCampaignRequestModel, FavoriteCampaignItemMessage>()
            .ForMember(dest => dest.Id, src => src.MapFrom(m => m.Id.ToString()));

    }
}