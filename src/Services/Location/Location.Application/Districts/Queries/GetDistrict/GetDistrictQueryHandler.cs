using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Location.Core.Entities;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Districts.Queries.GetDistrict
{
    public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, List<District>>
    {
        private readonly IDistrictRepository _districtRepository;

        public GetDistrictQueryHandler(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public async Task<List<District>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {
            return await _districtRepository.ByCityIdAsync(request.CityId);
        }
    }
}