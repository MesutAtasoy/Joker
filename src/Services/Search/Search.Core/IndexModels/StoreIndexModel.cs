using Joker.ElasticSearch.Models;

namespace Search.Core.IndexModels;

public class StoreIndexModel : ElasticEntity<Guid>
{
    public string Name { get; set; }
    public string Slogan { get; set; }
    public Guid MerchantId { get; set; }
    public string MerchantName { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public Guid CountryId { get; set; }
    public string CountryName { get; set; }
    public Guid CityId { get; set; }
    public string CityName { get; set; }
    public Guid DistrictId { get; set; }
    public string DistrictName { get; set; }
    public Guid NeighborhoodId { get; set; }
    public string NeighborhoodName { get; set; }
    public Guid QuarterId { get; set; }
    public string QuarterName { get; set; }
    public string Address { get; set; }
    public Guid OrganizationId { get; set; }

}