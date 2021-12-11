using Campaign.Application.Shared.Dto;

namespace Campaign.Application.Campaigns.Dto;

public class CampaignDto
{
    public Guid Id { get; set; }
    public IdNameDto Store { get; set; }
    public IdNameDto Merchant { get; set; }
    public IdNameDto BusinessDirectory { get; set; }
    public string Slug { get; set; }
    public string SlugKey { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid OrganizationId { get; set; }
}