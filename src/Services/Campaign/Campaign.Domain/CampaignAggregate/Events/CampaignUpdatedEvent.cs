using Campaign.Domain.Refs;
using Joker.Domain.DomainEvent;

namespace Campaign.Domain.CampaignAggregate.Events;

public class CampaignUpdatedEvent : DomainEvent
{
    private CampaignUpdatedEvent(){}
        
    public CampaignUpdatedEvent(Guid id,
        StoreRef store,
        BusinessDirectoryRef businessDirectory,
        string slug,
        string slugKey,
        string title,
        string code,
        string description,
        string condition,
        DateTime? startTime,
        DateTime? endTime)
    {
        Id = id;
        StoreId = store.RefId;
        StoreName = store.Name;
        BusinessDirectoryId = businessDirectory.RefId;
        BusinessDirectoryName = businessDirectory.Name;
        Slug = slug;
        SlugKey = slugKey;
        Title = title;
        Code = code;
        Description = description;
        Condition = condition;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Guid Id { get; private set; }
    public Guid StoreId { get; private set; }
    public string StoreName { get; private set; }
    public Guid BusinessDirectoryId { get; private set; }
    public string BusinessDirectoryName { get; private set; }
    public string Slug { get; private set; }
    public string SlugKey { get; private set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string Condition { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
}