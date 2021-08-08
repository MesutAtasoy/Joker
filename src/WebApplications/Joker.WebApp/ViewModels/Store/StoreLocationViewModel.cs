using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.ViewModels.Store
{
    public class StoreLocationViewModel
    {
        public IdNameViewModel Country { get; set; }
        public IdNameViewModel City { get; set; }
        public IdNameViewModel District { get; set; }
        public IdNameViewModel Quarter { get; set; }
        public IdNameViewModel Neighborhood { get; set; }
        public string Address { get; set; }
    }
}