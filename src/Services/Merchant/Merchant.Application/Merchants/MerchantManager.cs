using System;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Commands.DeleteMerchant;
using Merchant.Application.Merchants.Commands.UpdateMerchant;
using Merchant.Application.Merchants.Dto;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Merchants
{
    public class MerchantManager
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;

        public MerchantManager(IMerchantRepository merchantRepository, IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }

        public async Task<MerchantDto> CreateAsync(CreateMerchantCommand request)
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

            return _mapper.Map<MerchantDto>(merchant);
        }

        public async Task<MerchantDto> UpdateAsync(UpdateMerchantCommand request)
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

        public async Task<bool> DeleteAsync(DeleteMerchantCommand request)
        {
            var merchant = await _merchantRepository.GetByIdAsync(request.Id);

            if (merchant == null)
            {
                throw new NotFoundException("Merchant is not found");
            }
            
            merchant.MarkAsDeleted();

            await _merchantRepository.UpdateAsync(merchant.Id, merchant);
            
            return true;
        }

        public async Task<MerchantDto> GetByIdAsync(Guid id)
        {
            var merchant =  await _merchantRepository.GetByIdAsync(id);
            
            if (merchant == null)
            {
                throw new NotFoundException("Merchant is not found");
            }
            
            return _mapper.Map<MerchantDto>(merchant);
        }
    }
}