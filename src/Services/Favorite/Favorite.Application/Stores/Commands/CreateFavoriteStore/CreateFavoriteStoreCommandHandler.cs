using System.Threading;
using System.Threading.Tasks;
using Favorite.Application.Stores.Dto;
using MediatR;

namespace Favorite.Application.Stores.Commands.CreateFavoriteStore
{
    public class CreateFavoriteStoreCommandHandler : IRequestHandler<CreateFavoriteStoreCommand, FavoriteStoreDto>
    {
        private readonly FavoriteStoreManager _manager;
        
        public CreateFavoriteStoreCommandHandler(FavoriteStoreManager manager)
        {
            _manager = manager;
        }
        
        public async Task<FavoriteStoreDto> Handle(CreateFavoriteStoreCommand request, CancellationToken cancellationToken)
        {
            return await _manager.AddFavoriteStoreAsync(request);
        }
    }
}