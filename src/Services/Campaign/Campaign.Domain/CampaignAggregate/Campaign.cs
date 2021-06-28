using System;
using System.Collections.Generic;
using System.Linq;
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
            _campaignBadges = new List<CampaignBadge>();
            _campaignGalleries = new List<CampaignGallery>();
        }

        public Guid Id { get; private set; }
        public StoreRef Store { get; private set; }
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

        private List<CampaignBadge> _campaignBadges;
        public IReadOnlyList<CampaignBadge> CampaignBadges => _campaignBadges.AsReadOnly();

        private List<CampaignGallery> _campaignGalleries;
        public IReadOnlyList<CampaignGallery> CampaignGalleries => _campaignGalleries.AsReadOnly();


        /// <summary>
        /// Creates a new campaign
        /// </summary>
        /// <param name="id"></param>
        /// <param name="store"></param>
        /// <param name="title"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="condition"></param>
        /// <param name="previewImageUrl"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="channel"></param>
        /// <param name="galleries"></param>
        /// <returns></returns>
        public static Campaign Create(Guid id,
            StoreRef store,
            string title,
            string code,
            string description,
            string condition,
            string previewImageUrl,
            DateTime? startTime,
            DateTime? endTime,
            string channel,
            List<CampaignGallery> galleries)
        {
            Check.NotEmpty(id, nameof(id));
            Check.NotNull(store, nameof(store));
            Check.NotNullOrEmpty(title, nameof(title));

            var slugKey = IdGeneratorExtensions.GetNextIDThreadLocal();

            var campaign = new Campaign
            {
                Id = id,
                Store = store,
                Title = title,
                Code = code,
                Description = description,
                Condition = condition,
                PreviewImageUrl = previewImageUrl,
                StartTime = startTime,
                EndTime = endTime,
                Channel = channel,
                Slug = $"{title.GenerateSlug()}-{slugKey}",
                SlugKey = slugKey,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };

            if (galleries != null && galleries.Any())
            {
                campaign.AddImages(galleries);
            }

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

        /// <summary>
        /// Adds a new badge to campaign
        /// </summary>
        /// <param name="badge"></param>
        internal void AddBadge(CampaignBadge badge)
        {
            if (_campaignBadges.Any(x => x.Badge.RefId == badge.Badge.RefId))
            {
                return;
            }

            _campaignBadges.Add(badge);
            ModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes badge
        /// </summary>
        /// <param name="badge"></param>
        internal void RemoveBadge(CampaignBadge badge)
        {
            RemoveBadge(badge.Badge.RefId);
        }

        /// <summary>
        /// Removes badge
        /// </summary>
        /// <param name="badge"></param>
        internal void RemoveBadge(BadgeRef badge)
        {
            RemoveBadge(badge.RefId);
        }

        /// <summary>
        /// Removes badge
        /// </summary>
        /// <param name="badgeId"></param>
        internal void RemoveBadge(Guid badgeId)
        {
            var campaignBadge = _campaignBadges.FirstOrDefault(x => x.Badge.RefId == badgeId);
            if (campaignBadge == null)
            {
                return;
            }

            _campaignBadges.Remove(campaignBadge);
            ModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Add an image to campaign
        /// </summary>
        /// <param name="image"></param>
        /// <param name="order"></param>
        public void AddImage(string image, int? order)
        {
            Check.NotNullOrEmpty(image, nameof(image));

            var campaignGallery = new CampaignGallery(image, order);
            AddImage(campaignGallery);
        }

        /// <summary>
        /// Add an image to campaign
        /// </summary>
        /// <param name="gallery"></param>
        public void AddImage(CampaignGallery gallery)
        {
            _campaignGalleries.Add(gallery);
            ModifiedDate = DateTime.UtcNow;
        }
        
        
        /// <summary>
        /// Add an image to campaign
        /// </summary>
        /// <param name="galleries"></param>
        public void AddImages(List<CampaignGallery> galleries)
        {
            if (galleries == null || !galleries.Any())
            {
                return;
            }
            
            _campaignGalleries.AddRange(galleries);
            ModifiedDate = DateTime.UtcNow;
        }
    }
}