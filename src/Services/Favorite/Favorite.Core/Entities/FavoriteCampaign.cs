using System;

namespace Favorite.Core.Entities
{
    public class FavoriteCampaign
    {
        public Campaign Campaign { get; set; }
        public User UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}