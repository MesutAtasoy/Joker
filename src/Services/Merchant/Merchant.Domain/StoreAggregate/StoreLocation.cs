using Joker.Domain.Entities;
using Joker.Exceptions;
using Merchant.Domain.Refs;

namespace Merchant.Domain.StoreAggregate;

public class StoreLocation : DomainEntity
{
    private StoreLocation() { }
        
    public StoreLocation(CountryRef country,
        CityRef city,
        NeighborhoodRef neighborhood,
        DistrictRef district,
        QuarterRef quarter, 
        string address)
    {
        Check.NotNull(country, nameof(country));
        Check.NotNull(city, nameof(city));
        Check.NotNull(neighborhood, nameof(neighborhood));
        Check.NotNull(district, nameof(district));
        Check.NotNull(quarter, nameof(quarter));
        Check.NotNullOrEmpty(address, nameof(address));
            
        Country = country;
        City = city;
        Neighborhood = neighborhood;
        District = district;
        Quarter = quarter;
        Address = address;
    }

    public CountryRef Country { get; private set; }
    public CityRef City { get; private set; }
    public NeighborhoodRef Neighborhood { get; private set; }
    public DistrictRef District { get; private set; }
    public QuarterRef Quarter { get; private set; }
    public string Address { get; private set; }
}