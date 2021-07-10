using System;

namespace Aggregator.Api.Models.Store
{
    public class UpdateStoreLocationModel
    {
        public Guid Id { get; set; }
        public StoreLocationModel Location { get; set; }
    }
}