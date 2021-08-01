using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.WebApp.ViewModels.Management;

namespace Joker.WebApp.Services.Abstract
{
    public interface IManagementApiService
    {
        Task<List<PricingPlanViewModel>> GetPricingPlansAsync();
        Task<PricingPlanViewModel> GetPricingPlanAsync(string slug);
    }
}