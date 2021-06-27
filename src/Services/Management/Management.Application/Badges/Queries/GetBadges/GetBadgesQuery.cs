using System.Collections.Generic;
using Management.Core.Entities;
using MediatR;

namespace Management.Application.Badges.Queries.GetBadges
{
    public class GetBadgesQuery : IRequest<List<Badge>>
    {
        
    }
}