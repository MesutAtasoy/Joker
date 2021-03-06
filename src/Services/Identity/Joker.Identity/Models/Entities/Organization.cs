namespace Joker.Identity.Models.Entities;

public class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; }
}