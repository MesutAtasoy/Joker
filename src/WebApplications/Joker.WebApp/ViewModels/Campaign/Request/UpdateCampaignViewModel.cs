namespace Joker.WebApp.ViewModels.Campaign.Request;

public class UpdateCampaignViewModel
{
    public Guid CampaignId { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public string PreviewImageUrl { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}