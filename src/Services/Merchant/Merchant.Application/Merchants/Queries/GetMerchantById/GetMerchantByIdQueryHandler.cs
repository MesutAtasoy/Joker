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
        private readonly MerchantManager _merchantManager;

        public GetMerchantByIdQueryHandler(MerchantManager merchantManager)
        {
            _merchantManager = merchantManager;
        }

        public async Task<MerchantDto> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
        {
            return await _merchantManager.GetByIdAsync(request.Id);
        }
    }
}