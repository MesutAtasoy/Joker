using System;

namespace Favorite.Application.Stores.Dto
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
    }
}