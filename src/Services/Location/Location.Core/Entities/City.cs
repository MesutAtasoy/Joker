namespace Location.Core.Entities
{
    public partial class City : BaseEntityModel
    {
        public City()
        {
            Districts = new HashSet<District>();
            Neighborhoods = new HashSet<Neighborhood>();
            Quarters = new HashSet<Quarter>();
        }

        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Neighborhood> Neighborhoods { get; set; }
        public virtual ICollection<Quarter> Quarters { get; set; }
    }
}
