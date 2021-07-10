using System;
using System.Collections.Generic;
using System.Linq;
using Campaign.Domain.CampaignAggregate.Events;
using Campaign.Domain.Refs;
using Joker.Domain;
using Joker.Domain.Entities;
using Joker.Exceptions;
using Joker.Extensions;

namespace Campaign.Domain.CampaignAggregate
{
    public class Campaign : DomainEntity, IAggregateRoot
    {
        private Campaign()
        {

        }

        public Guid Id { get; private set; }
        public StoreRef Store { get; private set; }
        public BusinessDirectoryRef BusinessDirectory { get; private set; }
        public string Slug { get; private set; }
        public string SlugKey { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Condition { get; private set; }
        public string PreviewImageUrl { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Channel { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }

        /// <summary>
        /// Creates a new campaign
        /// </summary>
        /// <param name="id"></param>
        /// <param name="store"></param>
        /// <param name="businessDirectory"></param>
        /// <param name="title"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="condition"></param>
        /// <param name="previewImageUrl"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static Campaign Create(Guid id,
            StoreRef store,
            BusinessDirectoryRef businessDirectory,
            string title,
            string code,
            string description,
            string condition,
            string previewImageUrl,
            DateTime? startTime,
            DateTime? endTime,
            string channel)
        {
            Check.NotEmpty(id, nameof(id));
            Check.NotNull(store, nameof(store));
            Check.NotNull(businessDirectory, nameof(businessDirectory));
            Check.NotNullOrEmpty(title, nameof(title));

            var slugKey = IdGeneratorExtensions.GetNextIDThreadLocal();
            var slug = $"{title.GenerateSlug()}-{slugKey}";
            var campaign = new Campaign
            {
                Id = id,
                Store = store,
                BusinessDirectory = businessDirectory,
                Title = title,
                Code = code,
                Description = description,
                Condition = condition,
                PreviewImageUrl = previewImageUrl,
                StartTime = startTime,
                EndTime = endTime,
                Channel = channel,
                Slug = slug,
                SlugKey = slugKey,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };
            
            campaign.AddDomainEvent(new CampaignCreatedEvent(id,
                store,
                businessDirectory,
                slug,
                slugKey,
                title,
                code,
                description,
                condition,
                previewImageUrl,
                startTime,
                endTime,
                channel));

            return campaign;
        }

        /// <summary>
        /// Updates a campaign
        /// </summary>
        /// <param name="title"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="condition"></param>
        /// <param name="previewImageUrl"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public void Update(string title,
            string code,
            string description,
            string condition,
            string previewImageUrl,
            DateTime? startTime,
            DateTime? endTime)
        {
            Check.NotNullOrEmpty(title, nameof(title));

            Title = title;
            Code = code;
            Description = description;
            Condition = condition;
            PreviewImageUrl = previewImageUrl;
            StartTime = startTime;
            EndTime = endTime;
            Slug = $"{title.GenerateSlug()}-{SlugKey}";
            ModifiedDate = DateTime.UtcNow;
            
            
            AddDomainEvent(new CampaignUpdatedEvent(Id,
                Store,
                BusinessDirectory,
                Slug,
                SlugKey,
                Title,
                Code,
                Description,
                Condition,
                PreviewImageUrl,
                StartTime,
                EndTime,
                Channel));
        }

        /// <summary>
        /// Mark as deleted
        /// </summary>
        /// <returns></returns>
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            ModifiedDate = DateTime.UtcNow;
            
            AddDomainEvent(new CampaignDeletedEvent(Id, Title));
        }
    }
}