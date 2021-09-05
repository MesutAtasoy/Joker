using Favorite.Application.Campaigns.Dto;
using Favorite.Application.Shared.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign
{
    public class CreateFavoriteCampaignCommand : IRequest<FavoriteCampaignDto>
    {
        public IdNameDto Campaign { get; set; }
    }
}