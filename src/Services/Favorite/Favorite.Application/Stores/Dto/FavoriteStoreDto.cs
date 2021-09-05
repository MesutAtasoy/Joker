using System;
using Favorite.Application.Shared.Dto;

namespace Favorite.Application.Stores.Dto
{
    public class FavoriteStoreDto
    {
        public IdNameDto Store { get; set; }
        public UserDto UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}