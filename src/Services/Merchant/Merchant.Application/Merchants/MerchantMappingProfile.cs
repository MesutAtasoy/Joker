using AutoMapper;
using Merchant.Application.Merchants.Dto;

namespace Merchant.Application.Merchants;

public class MerchantMappingProfile : Profile
{
    public MerchantMappingProfile()
    {
        CreateMap<Domain.MerchantAggregate.Merchant, MerchantDto>();
    }
}