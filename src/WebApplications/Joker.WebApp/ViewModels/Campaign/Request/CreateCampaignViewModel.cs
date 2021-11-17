using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Campaign.Request;

public class CreateCampaignViewModel
{
    public IdNameViewModel Store { get; set; }
    public IdNameViewModel Merchant { get; set; }
    public IdNameViewModel BusinessDirectory { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public string PreviewImageUrl { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Channel { get; set; }
}