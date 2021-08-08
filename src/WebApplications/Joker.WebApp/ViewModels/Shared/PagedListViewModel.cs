using System.Collections.Generic;

namespace Joker.WebApp.ViewModels.Shared
{
    public class PagedListViewModel<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}