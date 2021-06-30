using Merchant.Application.Shared.Dto;

namespace Merchant.Application.Stores.Dto
{
    public class StoreLocationDto
    {
        public IdNameDto Country { get; set; }
        public IdNameDto City { get; set; }
        public IdNameDto District { get; set; }
        public IdNameDto Quarter { get; set; }
        public IdNameDto Neighborhood { get; set; }
        public string Address { get; set; }
    }
}