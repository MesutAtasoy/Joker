using AutoMapper;
using Location.Application.Cities.Dto;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Cities.Queries.GetCity;

public class GetCityQueryHandler : IRequestHandler<GetCityQuery, List<CityDto>>
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public GetCityQueryHandler(ICityRepository cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<List<CityDto>> Handle(GetCityQuery request, CancellationToken cancellationToken)
    {
        var cities =  await _cityRepository.ByCountryIdAsync(request.CountryId);
        return _mapper.Map<List<CityDto>>(cities);
    }
}