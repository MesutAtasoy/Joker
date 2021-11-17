using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Favorite;

public class FavoriteStoreViewModel
{
    public FavoriteStoreDetailViewModel Store { get; set; }
    public UserViewModel UserInfo { get; set; }
    public DateTime CreatedDate { get; set; }
}
    
public class FavoriteStoreDetailViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
}