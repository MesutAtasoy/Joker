using AutoMapper;
using Location.Application.Cities.Dto;
using Location.Application.Countries.Dto;
using Location.Application.Districts.Dto;
using Location.Application.Neighborhoods.Dto;
using Location.Application.Quarters.Commands.Verification;
using Location.Application.Quarters.Dto;
using Location.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Location.Application;

public class LocationManager
{
    private readonly IQuarterRepository _quarterRepository;
    private readonly IMapper _mapper;

    public LocationManager(IQuarterRepository quarterRepository, 
        IMapper mapper)
    {
        _quarterRepository = quarterRepository;
        _mapper = mapper;
    }

    public async Task<LocationDto> ValidateAsync(LocationVerificationCommand request)
    {
        var quarter = await _quarterRepository.Get()
            .Where(c => c.CountryId == request.CountryId &&
                        c.CityId == request.CityId &&
                        c.DistrictId == request.DistrictId &&
                        c.NeighborhoodId == request.NeighborhoodId &&
                        c.Id == request.QuarterId)
            .Include(x=>x.Country)
            .Include(x=>x.City)
            .Include(x=>x.District)
            .Include(x=>x.Neighborhood)
            .FirstOrDefaultAsync();

        if (quarter == null)
        {
            return new LocationDto {IsValid = false};
        }

        return new LocationDto
        {
            Country = _mapper.Map<CountryDto>(quarter.Country),
            City = _mapper.Map<CityDto>(quarter.City),
            District = _mapper.Map<DistrictDto>(quarter.District),
            Neighborhood = _mapper.Map<NeighborhoodDto>(quarter.Neighborhood),
            Quarter = _mapper.Map<QuarterDto>(quarter),
            IsValid = true
        };
    }
}