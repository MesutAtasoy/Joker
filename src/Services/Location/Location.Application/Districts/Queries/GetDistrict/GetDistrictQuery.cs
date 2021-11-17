using Location.Application.Districts.Dto;
using MediatR;

namespace Location.Application.Districts.Queries.GetDistrict;

public class GetDistrictQuery : IRequest<List<DistrictDto>>
{
    public Guid CityId { get; set; }
}