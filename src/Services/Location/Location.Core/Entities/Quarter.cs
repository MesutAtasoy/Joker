namespace Location.Core.Entities
{
    public partial class Quarter : BaseEntityModel
    {
        public Quarter()
        {
        }
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public Guid NeighborhoodId { get; set; }
        public string Name { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual Neighborhood Neighborhood { get; set; }
    }
}
