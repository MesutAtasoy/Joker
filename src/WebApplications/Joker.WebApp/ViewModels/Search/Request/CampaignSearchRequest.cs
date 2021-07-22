namespace Joker.WebApp.ViewModels.Search.Request
{
    public class CampaignSearchRequest : SearchBaseRequest
    {
        public string StoreName  { get; set; }
        public string Slug { get; set; }
        public string SlugKey { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string PreviewImageUrl { get; set; }
    }
}