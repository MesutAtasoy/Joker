using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Subscription.Application.Subscriptions.Dto;
using Subscription.Domain.SubscriptionAggregate.Repositories;

namespace Subscription.Application.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, List<SubscriptionDto>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<List<SubscriptionDto>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_subscriptionRepository.Get()
                .Where(x => x.Merchant.RefId == request.MerchantId)
                .ProjectTo<SubscriptionDto>(_mapper.ConfigurationProvider)
                .ToList());
        }
    }
}