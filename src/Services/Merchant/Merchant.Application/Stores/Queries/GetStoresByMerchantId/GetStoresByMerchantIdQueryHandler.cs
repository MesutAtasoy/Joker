using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Queries.GetStoresByMerchantId
{
    public class GetStoresByMerchantIdQueryHandler : IRequestHandler<GetStoresByMerchantIdQuery, List<StoreListDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        
        public GetStoresByMerchantIdQueryHandler(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        
        public async Task<List<StoreListDto>> Handle(GetStoresByMerchantIdQuery request, CancellationToken cancellationToken)
        {
            var stores =  _storeRepository.Get()
                .Where(x => !x.IsDeleted && x.Merchant.RefId == request.MerchantId)
                .ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
                .ToList();

            return stores;
        }
    }
}