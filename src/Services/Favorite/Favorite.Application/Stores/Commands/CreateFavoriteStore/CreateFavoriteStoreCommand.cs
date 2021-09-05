using Favorite.Application.Shared.Dto;
using Favorite.Application.Stores.Dto;
using MediatR;

namespace Favorite.Application.Stores.Commands.CreateFavoriteStore
{
    public class CreateFavoriteStoreCommand : IRequest<FavoriteStoreDto>
    {
        public IdNameDto Store { get; set; }
    }
}