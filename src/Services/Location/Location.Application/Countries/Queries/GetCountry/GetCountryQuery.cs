using Location.Application.Countries.Dto;
using MediatR;

namespace Location.Application.Countries.Queries.GetCountry
{
    public class GetCountryQuery : IRequest<CountryDto>
    {
    }
}