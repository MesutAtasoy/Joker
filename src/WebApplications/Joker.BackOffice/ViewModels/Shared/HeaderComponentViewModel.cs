namespace Joker.BackOffice.ViewModels.Shared;

public class HeaderComponentViewModel
{
    public List<HeaderComponentItemViewModel> Headers { get; set; }
}

public class HeaderComponentItemViewModel
{
    public HeaderComponentItemViewModel()
    {
        
    }

    public HeaderComponentItemViewModel(int order, bool isActive, string title, string link)
    {
        Order = order;
        IsActive = isActive;
        Title = title;
        Link = link;
    }
    
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public string Title { get; set; }
    public string Link  { get; set; }
}