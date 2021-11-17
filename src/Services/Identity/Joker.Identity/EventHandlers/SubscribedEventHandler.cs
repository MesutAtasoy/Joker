using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Joker.Identity.Events;
using Joker.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Joker.Identity.EventHandlers;

public class SubscribedEventHandler : CAPIntegrationEventHandler<SubscribedEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JokerIdentityDbContext _dbContext;

    public SubscribedEventHandler(UserManager<ApplicationUser> userManager,
        JokerIdentityDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
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

            await _dbContext.OrganizationUsers.AddAsync(new OrganizationUser
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                OrganizationId = @event.Merchant.RefId,
                OrganizationName = @event.Merchant.Name
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}