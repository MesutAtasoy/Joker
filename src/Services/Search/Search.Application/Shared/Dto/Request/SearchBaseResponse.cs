using System.Collections.Generic;

namespace Search.Application.Shared.Dto.Request
{
    public class SearchBaseResponse<T>
    {
        public SearchBaseResponse()
        {
            
        }
        
        public SearchBaseResponse(long took, 
            int page,
            int pageSize,
            long totalDocumentCount, 
            List<T> documents)
        {
            Took = took;
            Page = page;
            PageSize = pageSize;
            TotalDocumentCount = totalDocumentCount;
            Documents = documents;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Took { get; set; }
        public long TotalDocumentCount { get; set; }
        public List<T> Documents { get; set; }
    }
}