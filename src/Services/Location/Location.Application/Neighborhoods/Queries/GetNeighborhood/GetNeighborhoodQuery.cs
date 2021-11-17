using Location.Application.Neighborhoods.Dto;
using MediatR;

namespace Location.Application.Neighborhoods.Queries.GetNeighborhood;

public class GetNeighborhoodQuery : IRequest<List<NeighborhoodDto>>
{
    public Guid DistrictId { get; set; }
}