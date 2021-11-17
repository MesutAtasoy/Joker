namespace Joker.WebApp.ViewModels.Favorite.Request;

public class AddFavoriteStoreViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}