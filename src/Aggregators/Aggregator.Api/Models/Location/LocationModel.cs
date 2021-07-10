using Aggregator.Api.Models.Shared;

namespace Aggregator.Api.Models.Location
{
    public class LocationModel
    {
        public IdName Country { get; set; }
        public IdName City { get; set; }
        public IdName District { get; set; }
        public IdName Neighborhood { get; set; }
        public IdName Quarter { get; set; }
        public bool IsValid { get; set; }
    }
}