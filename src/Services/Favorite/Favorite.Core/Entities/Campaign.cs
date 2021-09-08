using System;

namespace Favorite.Core.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
    }
}