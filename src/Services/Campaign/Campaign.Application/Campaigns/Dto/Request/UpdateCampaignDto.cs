namespace Campaign.Application.Campaigns.Dto.Request;

public class UpdateCampaignDto
{
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}