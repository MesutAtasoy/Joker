using Joker.BackOffice.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Joker.BackOffice.ViewComponents;

public class HeaderComponent: ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(HeaderComponentViewModel componentViewModel)
    {
        return Task.FromResult((IViewComponentResult) View("Default", componentViewModel));
    }
}