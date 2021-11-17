using Joker.WebApp.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Joker.WebApp.ViewComponents;

public class PagingComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(PagingComponentViewModel result)
    {
        return Task.FromResult((IViewComponentResult) View("Default", result));
    }
}