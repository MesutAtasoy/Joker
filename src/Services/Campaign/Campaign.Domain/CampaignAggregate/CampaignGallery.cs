using Joker.Domain.Entities;
using Joker.Exceptions;

namespace Campaign.Domain.CampaignAggregate
{
    public class CampaignGallery : DomainEntity
    {
        private CampaignGallery(){}
        
        public CampaignGallery(string imageUrl, int? order)
        {
            Check.NotNullOrEmpty(imageUrl, nameof(imageUrl));
            
            ImageUrl = imageUrl;
            Order = order;
        }

        public string ImageUrl { get; private set; }
        public int? Order { get; private set; }
    }
}