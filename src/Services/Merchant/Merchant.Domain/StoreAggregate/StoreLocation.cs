using Joker.Domain.Entities;
using Merchant.Domain.Refs;

namespace Merchant.Domain.StoreAggregate
{
    public class StoreLocation : DomainEntity
    {
        private StoreLocation(){}
        
        public StoreLocation(CountryRef country,
            CityRef city,
            NeighborhoodRef neighborhood,
            DistrictRef district,
            QuarterRef quarter)
        {
            Country = country;
            City = city;
            Neighborhood = neighborhood;
            District = district;
            Quarter = quarter;
        }

        public CountryRef Country { get; private set; }
        public CityRef City { get; private set; }
        public NeighborhoodRef Neighborhood { get; private set; }
        public DistrictRef District { get; private set; }
        public QuarterRef Quarter { get; private set; }
    }
}