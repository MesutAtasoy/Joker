using System;
using Favorite.Application.Shared.Dto;

namespace Favorite.Application.Campaigns.Dto
{
    public class FavoriteCampaignDto
    {
        public CampaignDto Campaign { get; set; }
        public UserDto UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}