using AutoMapper;
using Subscription.Application.Subscriptions.Dto;

namespace Subscription.Application.Subscriptions;

public class SubscriptionMappingProfile : Profile
{
    public SubscriptionMappingProfile()
    {
        CreateMap<Domain.SubscriptionAggregate.Subscription, SubscriptionDto>();
    }
}