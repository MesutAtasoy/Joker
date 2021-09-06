using System;
using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Store
{
    public class StoreModel
    {
        public Guid Id { get;  set; }
        public IdNameModel Merchant { get;  set; }
        public string Name { get;  set; }
        public string Slogan { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Email { get;  set; }
        public bool EmailConfirmed { get;  set; }
        public string Description { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public DateTime? ModifiedDate { get;  set; }
        public StoreLocationModel Location { get; set; }
    }
}