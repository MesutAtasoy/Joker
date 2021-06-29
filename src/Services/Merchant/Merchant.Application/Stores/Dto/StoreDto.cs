using System.Collections.Generic;

namespace Merchant.Application.Stores.Dto
{
    public class StoreDto : StoreListDto
    {
        public List<StoreFAQDto> StoreFAQs { get; set; }
        public List<StoreBusinessHourDto> StoreBusinessHours { get; set; }
    }
}