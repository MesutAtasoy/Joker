namespace Management.Core.Entities
{
    public partial class SocialProvider : BaseEntityModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string ImageUrl { get; set; }
    }
}