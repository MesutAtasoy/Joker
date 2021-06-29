using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Merchant.Application.Merchants.Dto;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, MerchantDto>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;

        public CreateMerchantCommandHandler(IMerchantRepository merchantRepository, 
            IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }

        public async Task<MerchantDto> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchantId = IdGenerationFactory.Create();

            var merchant = Domain.MerchantAggregate.Merchant.Create(merchantId,
                request.Name,
                request.Slogan,
                request.WebSiteUrl,
                request.PhoneNumber,
                request.TaxNumber,
                request.Email,
                request.Description);

            await _merchantRepository.AddAsync(merchant);

            return _mapper.Map<MerchantDto>(merchant);;
        }
    }
}