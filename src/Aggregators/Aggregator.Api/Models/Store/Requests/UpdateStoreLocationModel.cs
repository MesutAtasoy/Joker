namespace Aggregator.Api.Models.Store.Requests;

public class UpdateStoreLocationModel
{
    public Guid Id { get; set; }
    public StoreLocationModel Location { get; set; }
}