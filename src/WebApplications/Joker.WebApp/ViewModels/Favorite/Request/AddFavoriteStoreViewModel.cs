namespace Joker.WebApp.ViewModels.Favorite.Request;

public class AddFavoriteStoreViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OrganizationId { get; set; }
}