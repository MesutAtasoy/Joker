using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Joker.Extensions;
using Joker.Extensions.Models;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Queries.GetStoresByMerchantId
{
    public class GetStoresByMerchantIdQueryHandler : IRequestHandler<GetStoresByMerchantIdQuery, PagedList<StoreDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        
        public GetStoresByMerchantIdQueryHandler(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        
        public async Task<PagedList<StoreDto>> Handle(GetStoresByMerchantIdQuery request, CancellationToken cancellationToken)
        {
            var stores = _storeRepository.Get()
                .Where(x => !x.IsDeleted && x.Merchant.RefId == request.MerchantId)
                .ProjectTo<StoreDto>(_mapper.ConfigurationProvider)
                .ToPagedList(request.Filter);
            
            return stores;
        }
    }
}