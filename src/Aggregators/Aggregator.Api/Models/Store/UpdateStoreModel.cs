using System;

namespace Aggregator.Api.Models.Store
{
    public class UpdateStoreModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}