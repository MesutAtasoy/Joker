using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, StoreDto>
    {
        private readonly StoreManager _storeManager;

        public GetStoreByIdQueryHandler(StoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        public async Task<StoreDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            return await _storeManager.GetByIdAsync(request.Id);
        }
    }
}