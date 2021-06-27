using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, Guid>
    {
        private readonly IMerchantRepository _merchantRepository;

        public CreateMerchantCommandHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Guid> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
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
            
            return merchantId;
        }
    }
}