namespace Joker.BackOffice.ViewModels.Store.Request;

public class CreateStoreViewModel
{
    public Guid MerchantId { get; set; }
    public string Name { get; set; }
    public string Slogan { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public StoreLocationViewModel Location { get; set; }
}