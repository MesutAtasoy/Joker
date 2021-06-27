using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Merchant.Application.Merchants.Dto;
using Merchant.Domain.MerchantAggregate.Repositories;

namespace Merchant.Application.Merchants.Queries.GetMerchantById
{
    public class GetMerchantByIdQueryHandler : IRequestHandler<GetMerchantByIdQuery, MerchantDto>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;

        public GetMerchantByIdQueryHandler(IMerchantRepository merchantRepository, 
            IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }

        public async Task<MerchantDto> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetByIdAsync(request.Id);
            return _mapper.Map<MerchantDto>(merchant);
        }
    }
}