using Joker.WebApp.ViewModels.Merchant;
using Joker.WebApp.ViewModels.Merchant.Request;
using Joker.WebApp.ViewModels.Shared;
using Joker.WebApp.ViewModels.Store;
using Joker.WebApp.ViewModels.Store.Request;

namespace Joker.WebApp.Services.Abstract;

public interface IMerchantService
{
    Task<JokerBaseResponseViewModel<MerchantViewModel>> CreateAsync(CreateMerchantViewModel createMerchantViewModel);
    Task<JokerBaseResponseViewModel<MerchantViewModel>> UpdateAsync(UpdateMerchantViewModel updateMerchantViewModel);
    Task<MerchantViewModel> GetByIdAsync(Guid id);
    Task<PagedListViewModel<StoreViewModel>> GetStoresAsync(Guid merchantId, int page = 1, int pageSize = 20);
    Task<JokerBaseResponseViewModel<StoreViewModel>> CreateStoreAsync(CreateStoreViewModel createStoreViewModel);
    Task<JokerBaseResponseViewModel<StoreViewModel>> UpdateStoreAsync(UpdateStoreViewModel updateStoreViewModel);
    Task<StoreViewModel> GetStoreByIdAsync(Guid id);
}