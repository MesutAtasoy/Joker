using System;
using Favorite.Application.Campaigns.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign
{
    public class CreateFavoriteCampaignCommand : IRequest<FavoriteCampaignDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
        public Guid OrganizationId { get; set; }
    }
}