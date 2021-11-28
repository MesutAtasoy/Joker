using Joker.Domain;
using Joker.Domain.Entities;
using Joker.Exceptions;
using Joker.Extensions;
using Merchant.Domain.MerchantAggregate.Events;

namespace Merchant.Domain.MerchantAggregate;

public class Merchant : DomainEntity, IAggregateRoot
{
    private Merchant()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Slogan { get; private set; }
    public string Slug { get; private set; }
    public string SlugKey { get; private set; }
    public string WebSiteUrl { get; private set; }
    public string PhoneNumber { get; private set; }
    public string TaxNumber { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public Guid OrganizationId { get; private set; }

    
    /// <summary>
    /// Creates a new merchant
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="slogan"></param>
    /// <param name="webSiteUrl"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="taxNumber"></param>
    /// <param name="email"></param>
    /// <param name="description"></param>
    /// <param name="pricingPlanId"></param>
    /// <param name="pricingPlanName"></param>
    /// <param name="userId"></param>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    public static Merchant Create(Guid id,
        string name,
        string slogan,
        string webSiteUrl,
        string phoneNumber,
        string taxNumber,
        string email,
        string description,
        Guid pricingPlanId,
        string pricingPlanName,
        Guid userId,
        Guid organizationId)
    {
        Check.NotEmpty(id, nameof(id));
        Check.NotEmpty(pricingPlanId, nameof(pricingPlanId));
        Check.NotEmpty(userId, nameof(userId));
        Check.NotNullOrEmpty(name, nameof(name));
        Check.NotNullOrEmpty(slogan, nameof(slogan));
        Check.NotNullOrEmpty(pricingPlanName, nameof(pricingPlanName));
        Check.NotEmpty(organizationId, nameof(organizationId));

        var slugKey = IdGeneratorExtensions.GetNextIDThreadLocal();

        var merchant = new Merchant
        {
            Id = id,
            Name = name,
            Slogan = slogan,
            SlugKey = slugKey,
            Slug = $"{name.GenerateSlug()}-{slugKey}",
            WebSiteUrl = webSiteUrl,
            PhoneNumber = phoneNumber,
            TaxNumber = taxNumber,
            Email = email,
            Description = description,
            EmailConfirmed = false,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
            OrganizationId = organizationId
        };
        
        merchant.AddDomainEvent(new MerchantCreatedEvent(id, name, pricingPlanId, pricingPlanName, userId, organizationId));
            
        return merchant;
    }


    /// <summary>
    /// Updates a merchant
    /// </summary>
    /// <param name="name"></param>
    /// <param name="slogan"></param>
    /// <param name="webSiteUrl"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="taxNumber"></param>
    /// <param name="email"></param>
    /// <param name="description"></param>
    public void Update(string name,
        string slogan,
        string webSiteUrl,
        string phoneNumber,
        string taxNumber,
        string email,
        string description)
    {
        Check.NotNullOrEmpty(name, nameof(name));
        Check.NotNullOrEmpty(slogan, nameof(slogan));

        if (Name != name)
        {
            AddDomainEvent(new MerchantNameUpdatedEvent(Id, Name, name));
        }

        Name = name;
        Slug = $"{name.GenerateSlug()}-{SlugKey}";
        Slogan = slogan;
        WebSiteUrl = webSiteUrl;
        PhoneNumber = phoneNumber;
        TaxNumber = taxNumber;
        Description = description;
        ModifiedDate = DateTime.UtcNow;
        if (Email != email)
        {
            EmailConfirmed = false;
        }

        Email = email;
    }

    /// <summary>
    /// Mark as confirmed mail 
    /// </summary>
    public void MarkAsConfirmedMail()
    {
        EmailConfirmed = true;
        ModifiedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Mark as deleted
    /// </summary>
    /// <returns></returns>
    public void MarkAsDeleted()
    {
        IsDeleted = true;
        ModifiedDate = DateTime.UtcNow;
    }
}