using Joker.WebApp.ViewModels.Merchant;
using Joker.WebApp.ViewModels.Merchant.Request;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract;

public interface IMerchantService
{
    Task<JokerBaseResponseViewModel<MerchantViewModel>> CreateAsync(CreateMerchantViewModel createMerchantViewModel);
}