using System;

namespace Favorite.Core.Entities
{
    public class FavoriteStore
    {
        public Store Store { get; set; }
        public User UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}