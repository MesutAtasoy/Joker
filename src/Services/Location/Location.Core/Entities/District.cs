namespace Location.Core.Entities
{
    public partial class District : BaseEntityModel
    {
        public District()
        {
            Neighborhoods = new HashSet<Neighborhood>();
            Quarters = new HashSet<Quarter>();
        }

        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Neighborhood> Neighborhoods { get; set; }
        public virtual ICollection<Quarter> Quarters { get; set; }
    }
}
