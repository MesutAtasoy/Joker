using System;
using System.Collections.Generic;

namespace Merchant.Application.Stores.Dto
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<StoreFAQDto> StoreFAQs { get; set; }
        public List<StoreBusinessHourDto> StoreBusinessHours { get; set; }
    }
}