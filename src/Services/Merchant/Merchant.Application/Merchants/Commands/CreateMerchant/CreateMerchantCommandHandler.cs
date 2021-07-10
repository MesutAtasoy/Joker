using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Merchant.Application.Merchants.Dto;

namespace Merchant.Application.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, MerchantDto>
    {
        private readonly MerchantManager _merchantManager;

        public CreateMerchantCommandHandler(MerchantManager merchantManager)
        {
            _merchantManager = merchantManager;
        }

        public async Task<MerchantDto> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            return await _merchantManager.CreateAsync(request);
        }
    }
}