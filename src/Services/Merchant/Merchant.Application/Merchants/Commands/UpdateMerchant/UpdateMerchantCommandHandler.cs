using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using MediatR;
using Merchant.Application.Merchants.Dto;
using Merchant.Domain.MerchantAggregate.Repositories;

namespace Merchant.Application.Merchants.Commands.UpdateMerchant
{
    public class UpdateMerchantCommandHandler : IRequestHandler<UpdateMerchantCommand, MerchantDto>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;

        public UpdateMerchantCommandHandler(IMerchantRepository merchantRepository, 
            IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }

        public async Task<MerchantDto> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetByIdAsync(request.Id);
            if (merchant == null)
            {
                throw new NotFoundException("Merchant is not found");
            }
            
            merchant.Update(request.Merchant.Name, 
                request.Merchant.Slogan,
                request.Merchant.WebSiteUrl,
                request.Merchant.PhoneNumber,
                request.Merchant.TaxNumber,
                request.Merchant.Email,
                request.Merchant.Description);

            await _merchantRepository.UpdateAsync(merchant.Id, merchant);

            return _mapper.Map<MerchantDto>(merchant);
        }
    }
}