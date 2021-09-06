using System;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Favorite
{
    public class FavoriteStoreViewModel
    {
        public IdNameViewModel Store { get; set; }
        public UserViewModel UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}