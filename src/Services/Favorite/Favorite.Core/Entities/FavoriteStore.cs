using System;
using Favorite.Core.Entities.Shared;

namespace Favorite.Core.Entities
{
    public class FavoriteStore
    {
        public IdNameRef Store { get; set; }
        public User UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}