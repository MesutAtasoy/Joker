using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Favorite.Application.Campaigns.Events;
using Favorite.Core.Entities;
using Favorite.Core.Repositories;
using Joker.CAP.IntegrationEvent;
using Joker.EventBus;
using Microsoft.Extensions.Logging;

namespace Favorite.Application.Campaigns.EventHandlers
{
    public class CampaignCreatedEventHandler : CAPIntegrationEventHandler<CampaignCreatedEvent>
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IFavoriteStoreRepository _storeRepository;

        public CampaignCreatedEventHandler(IFavoriteStoreRepository storeRepository,
            IEventDispatcher eventDispatcher)
        {
            _storeRepository = storeRepository;
            _eventDispatcher = eventDispatcher;
        }

        [CapSubscribe(nameof(CampaignCreatedEvent))]
        public override async Task Handle(CampaignCreatedEvent @event)
        {
            var favoriteStores = await _storeRepository.GetStoresByStoreIdAsync(@event.StoreId.ToString());

            var users = favoriteStores.Select(x => x.UserInfo);

            var userStack = new Stack<User>(users);
            while (userStack.Count > 0)
            {
                var items = Enumerable.Range(0, 10)
                    .Select(f => userStack.Count > 0 ? userStack.Pop() : null)
                    .Where(s => s != null)
                    .Select(f => _eventDispatcher.Dispatch(CreateEvent(f,  
                        @event.StoreId,
                        @event.StoreName,
                        @event.Id,
                        @event.Title,
                        @event.Slug)))
                    .ToArray();

                await Task.WhenAll(items);
            }
        }

        private CampaignCreatedNotificationEvent CreateEvent(User user, 
            Guid storeId,
            string storeName,
            Guid campaignId,
            string title,
            string slug)
        {
            return new CampaignCreatedNotificationEvent(storeId, storeName,campaignId, title, slug,user.Id, user.Username);
        }
    }
}