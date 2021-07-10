using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Store
{
    public class StoreLocationModel
    {
        public IdName Country { get; set; }
        public IdName City { get; set; }
        public IdName District { get; set; }
        public IdName Quarter { get; set; }
        public IdName Neighborhood { get; set; }
        public string Address { get; set; }
    }
}