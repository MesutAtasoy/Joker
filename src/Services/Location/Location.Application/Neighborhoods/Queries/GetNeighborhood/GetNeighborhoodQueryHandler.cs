using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Location.Core.Entities;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Neighborhoods.Queries.GetNeighborhood
{
    public class GetNeighborhoodQueryHandler : IRequestHandler<GetNeighborhoodQuery, List<Neighborhood>>
    {
        private readonly INeighborhoodRepository _neighborhoodRepository;

        public GetNeighborhoodQueryHandler(INeighborhoodRepository neighborhoodRepository)
        { 
            _neighborhoodRepository = neighborhoodRepository;
        }

        public async Task<List<Neighborhood>> Handle(GetNeighborhoodQuery request, CancellationToken cancellationToken)
        {
            return await _neighborhoodRepository.ByDistrictIdAsync(request.DistrictId);
        }
    }
}