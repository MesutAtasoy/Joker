using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, StoreDto>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public GetStoreByIdQueryHandler(IStoreRepository storeRepository, 
            IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.Id);
            return _mapper.Map<StoreDto>(store);
        }
    }
}