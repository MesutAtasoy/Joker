namespace Joker.BackOffice.ViewModels.Shared;

public class PagingParameters
{
    public int CurrentPage { get; set; }
    public int PageCount  { get; set; }
    public int PageSize  { get; set; }
    public int RowCount  { get; set; }
    public string LinkTemplate { get; set; }
}