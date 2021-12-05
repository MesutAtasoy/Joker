using Joker.BackOffice.ViewModels.Merchant;
using Joker.BackOffice.ViewModels.Shared;
using Joker.BackOffice.ViewModels.Store;
using Joker.BackOffice.ViewModels.Store.Request;

namespace Joker.BackOffice.Services.Abstract;

public interface IMerchantService
{
    Task<List<MerchantViewModel>> GetMerchants();
    Task<PagedListViewModel<StoreViewModel>> GetStoresAsync(int page = 1, int pageSize = 20);
    Task<PagedListViewModel<StoreViewModel>> GetStoresAsync(Guid merchantId, int page = 1, int pageSize = 20);
    Task<JokerBaseResponseViewModel<StoreViewModel>> CreateStoreAsync(CreateStoreViewModel createStoreViewModel);
    Task<JokerBaseResponseViewModel<StoreViewModel>> UpdateStoreAsync(UpdateStoreViewModel updateStoreViewModel);
    Task<StoreViewModel> GetStoreByIdAsync(Guid id);
}