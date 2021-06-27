using Location.Core.Entities;
using MediatR;

namespace Location.Application.Countries.Queries.GetCountry
{
    public class GetCountryQuery : IRequest<Country>
    {
        public string Name { get; set; }
    }
}