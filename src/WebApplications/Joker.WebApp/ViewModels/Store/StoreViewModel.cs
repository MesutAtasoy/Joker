using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Store;

public class StoreViewModel
{
    public Guid Id { get;  set; }
    public RefIdNameViewModel Merchant { get;  set; }
    public string Name { get;  set; }
    public string Slogan { get;  set; }
    public string PhoneNumber { get;  set; }
    public string Email { get;  set; }
    public bool EmailConfirmed { get;  set; }
    public string Description { get;  set; }
    public DateTime CreatedDate { get;  set; }
    public DateTime? ModifiedDate { get;  set; }
    public StoreLocationViewModel Location { get; set; }
}