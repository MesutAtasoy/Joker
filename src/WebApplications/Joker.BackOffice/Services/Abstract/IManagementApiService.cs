using Joker.BackOffice.ViewModels.Management;

namespace Joker.BackOffice.Services.Abstract;

public interface IManagementApiService
{
    Task<List<PricingPlanViewModel>> GetPricingPlansAsync();
    Task<List<BusinessDirectoryViewModel>> GetBusinessDirectoriesAsync();
    Task<PricingPlanViewModel> GetPricingPlanAsync(string slug);
}