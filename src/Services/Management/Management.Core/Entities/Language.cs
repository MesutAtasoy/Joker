namespace Management.Core.Entities
{
    public partial class Language : BaseEntityModel
    {
        public Language()
        {
        }

        public string Code { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
