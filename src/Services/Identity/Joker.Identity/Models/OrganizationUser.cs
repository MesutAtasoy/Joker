namespace Joker.Identity.Models;

public class OrganizationUser
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public virtual ApplicationUser User { get; set; }
}