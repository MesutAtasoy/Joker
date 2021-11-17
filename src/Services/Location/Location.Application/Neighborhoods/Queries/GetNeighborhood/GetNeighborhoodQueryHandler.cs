using AutoMapper;
using Location.Application.Neighborhoods.Dto;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Neighborhoods.Queries.GetNeighborhood;

public class GetNeighborhoodQueryHandler : IRequestHandler<GetNeighborhoodQuery, List<NeighborhoodDto>>
{
    private readonly INeighborhoodRepository _neighborhoodRepository;
    private readonly IMapper _mapper;

    public GetNeighborhoodQueryHandler(INeighborhoodRepository neighborhoodRepository,
        IMapper mapper)
    {
        _neighborhoodRepository = neighborhoodRepository;
        _mapper = mapper;
    }

    public async Task<List<NeighborhoodDto>> Handle(GetNeighborhoodQuery request, CancellationToken cancellationToken)
    {
        var neighborhoods =  await _neighborhoodRepository.ByDistrictIdAsync(request.DistrictId);
        return _mapper.Map<List<NeighborhoodDto>>(neighborhoods);
    }
}