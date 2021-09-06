using System;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Favorite
{
    public class FavoriteCampaignViewModel
    {
        public IdNameViewModel Campaign { get; set; }
        public UserViewModel UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}