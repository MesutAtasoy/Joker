namespace Joker.BackOffice.ViewModels.Shared;

public class PagingComponentViewModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
}