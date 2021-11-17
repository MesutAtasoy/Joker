using Location.Application.Quarters.Dto;
using MediatR;

namespace Location.Application.Quarters.Commands.Verification;

public class LocationVerificationCommand : IRequest<LocationDto>
{
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
    public Guid NeighborhoodId { get; set; }
    public Guid QuarterId { get; set; }
}