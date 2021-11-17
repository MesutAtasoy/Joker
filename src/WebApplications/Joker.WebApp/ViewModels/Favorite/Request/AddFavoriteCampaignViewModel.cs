namespace Joker.WebApp.ViewModels.Favorite.Request;

public class AddFavoriteCampaignViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}