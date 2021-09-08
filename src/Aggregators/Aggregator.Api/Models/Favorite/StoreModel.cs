using System;

namespace Aggregator.Api.Models.Favorite
{
    public class StoreModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
    }
}