using Joker.WebApp.ViewModels.Management;

namespace Joker.WebApp.Services.Abstract;

public interface IManagementApiService
{
    Task<List<PricingPlanViewModel>> GetPricingPlansAsync();
    Task<List<BusinessDirectoryViewModel>> GetBusinessDirectoriesAsync();
    Task<PricingPlanViewModel> GetPricingPlanAsync(string slug);
}