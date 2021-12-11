using Joker.BackOffice.ViewModels.Shared;

namespace Joker.BackOffice.ViewModels.Campaign;

public class CampaignViewModel
{
    public Guid Id { get; set; }
    public RefIdNameViewModel Store { get; set; }
    public RefIdNameViewModel Merchant { get; set; }
    public RefIdNameViewModel BusinessDirectory { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatedDate { get; set; }
}