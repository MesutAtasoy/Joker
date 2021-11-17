using Location.Application.Cities.Dto;
using MediatR;

namespace Location.Application.Cities.Queries.GetCity;

public class GetCityQuery : IRequest<List<CityDto>>
{
    public Guid CountryId { get; set; }
}