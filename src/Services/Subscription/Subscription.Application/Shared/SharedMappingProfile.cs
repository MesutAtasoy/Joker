using AutoMapper;
using Subscription.Application.Shared.Dto;
using Subscription.Domain.Refs;

namespace Subscription.Application.Shared
{
    public class SharedMappingProfile : Profile
    {
        public SharedMappingProfile()
        {
            CreateMap<PricingPlanRef, IdNameDto>();
            CreateMap<MerchantRef, IdNameDto>();
        }
    }
}