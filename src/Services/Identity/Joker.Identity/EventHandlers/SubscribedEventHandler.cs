using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Joker.Identity.Events;
using Joker.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Joker.Identity.EventHandlers
{
    public class SubscribedEventHandler : CAPIntegrationEventHandler<SubscribedEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SubscribedEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [CapSubscribe(nameof(SubscribedEvent))]
        public override async Task Handle(SubscribedEvent @event)
        {
            var user = await _userManager.FindByIdAsync(@event.UserId.ToString());
            if (user == null)
            {
                return;
            }

            var isInRole = await _userManager.IsInRoleAsync(user, "PaidUser");
            if (!isInRole)
            {
                await _userManager.AddToRoleAsync(user, "PaidUser");
            }
        }
    }
}