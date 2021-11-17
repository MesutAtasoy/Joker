using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Favorite;

public class FavoriteCampaignViewModel
{
    public FavoriteCampaignDetailViewModel Campaign { get; set; }
    public UserViewModel UserInfo { get; set; }
    public DateTime CreatedDate { get; set; }
}
    
public class FavoriteCampaignDetailViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}