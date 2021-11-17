using Joker.Domain.DomainEvent;
using Merchant.Domain.Refs;

namespace Merchant.Domain.StoreAggregate.Events;

public class StoreCreatedEvent : DomainEvent
{
    public StoreCreatedEvent(Guid id,
        MerchantRef merchant,
        string name,
        string slogan,
        string phoneNumber,
        string email,
        string description,
        StoreLocation location)
    {
        Id = id;
        MerchantId = merchant.RefId;
        MerchantName = merchant.Name;
        Name = name;
        Slogan = slogan;
        PhoneNumber = phoneNumber;
        Email = email;
        Description = description;
        CountryId = location.Country.RefId;
        CountryName = location.Country.Name;
        CityId = location.City.RefId;
        CityName= location.City.Name;
        DistrictId = location.District.RefId;
        DistrictName = location.District.Name;
        NeighborhoodId = location.Neighborhood.RefId;
        NeighborhoodName = location.Neighborhood.Name;
        QuarterId = location.Quarter.RefId;
        QuarterName = location.Quarter.Name;
        Address = location.Address;
    }

    public Guid Id { get; private set; }
    public Guid MerchantId { get; private set; }
    public string MerchantName { get; private set; }
    public string Name { get; private set; }
    public string Slogan { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public Guid CountryId { get; private set; }
    public string CountryName { get; private set; }
    public Guid CityId { get; private set; }
    public string CityName { get; private set; }
    public Guid DistrictId { get; private set; }
    public string DistrictName { get; private set; }
    public Guid NeighborhoodId { get; private set; }
    public string NeighborhoodName { get; private set; }
    public Guid QuarterId { get; private set; }
    public string QuarterName { get; private set; }
    public string Address { get; private set; }
}