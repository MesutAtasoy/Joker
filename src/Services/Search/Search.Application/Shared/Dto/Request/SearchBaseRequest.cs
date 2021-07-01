namespace Search.Application.Shared.Dto.Request
{
    public class SearchBaseRequest
    {
        public string q { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Order { get; set; }
    }
}