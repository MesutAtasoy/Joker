using System;
using Favorite.Application.Stores.Dto;
using MediatR;

namespace Favorite.Application.Stores.Commands.CreateFavoriteStore
{
    public class CreateFavoriteStoreCommand : IRequest<FavoriteStoreDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
        public Guid OrganizationId { get; set; }
    }
}