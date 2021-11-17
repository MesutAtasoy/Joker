namespace Joker.Identity.Models;

public class OrganizationUser
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public string OrganizationName { get; set; }
    public virtual ApplicationUser User { get; set; }
}