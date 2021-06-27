using System;

namespace Merchant.Application.Stores.Dto
{
    public class StoreFAQDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
    }
}