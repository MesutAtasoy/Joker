using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.Badges.Queries.GetBadges
{
    public class GetBadgesQueryHandler : IRequestHandler<GetBadgesQuery, List<Badge>>
    {
        private readonly IBadgeRepository _badgeRepository;

        public GetBadgesQueryHandler(IBadgeRepository badgeRepository)
        {
            _badgeRepository = badgeRepository;
        }

        public async Task<List<Badge>> Handle(GetBadgesQuery request, CancellationToken cancellationToken)
        {
            var badges = _badgeRepository.Get(x => !x.IsDeleted)
                .ToList();

            return badges;
        }
    }
}