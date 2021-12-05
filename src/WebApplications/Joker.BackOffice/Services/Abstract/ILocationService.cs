using Joker.BackOffice.ViewModels.Shared;

namespace Joker.BackOffice.Services.Abstract;

public interface ILocationService
{
    Task<IdNameViewModel> GetCountryAsync();
    Task<List<IdNameViewModel>> GetCitiesAsync(Guid countryId);
    Task<List<IdNameViewModel>> GetDistrictsAsync(Guid cityId);
    Task<List<IdNameViewModel>> GetNeighborhoodsAsync(Guid districtId);
    Task<List<IdNameViewModel>> GetQuartersAsync(Guid neighborhoodId);
}