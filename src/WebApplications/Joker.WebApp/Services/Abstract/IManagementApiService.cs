using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.WebApp.ViewModels;
using Joker.WebApp.ViewModels.Management;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract
{
    public interface IManagementApiService
    {
        Task<List<PricingPlanViewModel>> GetPricingPlansAsync();
        Task<List<BusinessDirectoryViewModel>> GetBusinessDirectoriesAsync();
        Task<PricingPlanViewModel> GetPricingPlanAsync(string slug);
        Task<IdNameViewModel> GetCountryAsync();
        Task<List<IdNameViewModel>> GetCitiesAsync(Guid countryId);
        Task<List<IdNameViewModel>> GetDistrictsAsync(Guid cityId);
        Task<List<IdNameViewModel>> GetNeighborhoodsAsync(Guid districtId);
        Task<List<IdNameViewModel>> GetQuartersAsync(Guid neighborhoodId);
    }
}