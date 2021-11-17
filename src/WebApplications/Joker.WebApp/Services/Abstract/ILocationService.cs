using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract;

public interface ILocationService
{
    Task<IdNameViewModel> GetCountryAsync();
    Task<List<IdNameViewModel>> GetCitiesAsync(Guid countryId);
    Task<List<IdNameViewModel>> GetDistrictsAsync(Guid cityId);
    Task<List<IdNameViewModel>> GetNeighborhoodsAsync(Guid districtId);
    Task<List<IdNameViewModel>> GetQuartersAsync(Guid neighborhoodId);
}