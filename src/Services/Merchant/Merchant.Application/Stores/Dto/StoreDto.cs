using Merchant.Application.Shared.Dto;

namespace Merchant.Application.Stores.Dto;

public class StoreDto
{
    public Guid Id { get;  set; }
    public IdNameDto Merchant { get;  set; }
    public string Name { get;  set; }
    public string Slogan { get;  set; }
    public string PhoneNumber { get;  set; }
    public string Email { get;  set; }
    public bool EmailConfirmed { get;  set; }
    public string Description { get;  set; }
    public DateTime CreatedDate { get;  set; }
    public DateTime? ModifiedDate { get;  set; }
    public StoreLocationDto Location { get; set; }
}