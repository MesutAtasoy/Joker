using AutoMapper;
using Location.Application.Districts.Dto;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Districts.Queries.GetDistrict;

public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, List<DistrictDto>>
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IMapper _mapper;

    public GetDistrictQueryHandler(IDistrictRepository districtRepository,
        IMapper mapper)
    {
        _districtRepository = districtRepository;
        _mapper = mapper;
    }

    public async Task<List<DistrictDto>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
    {
        var districts =  await _districtRepository.ByCityIdAsync(request.CityId);
        return _mapper.Map<List<DistrictDto>>(districts);
    }
}