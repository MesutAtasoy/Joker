using Joker.WebApp.ViewModels.Management;

namespace Joker.WebApp.Services.Abstract;

public interface IManagementService
{
    Task<List<PricingPlanViewModel>> GetPricingPlansAsync();
    Task<PricingPlanViewModel> GetPricingPlanAsync(string slug);
}