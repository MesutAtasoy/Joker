using System.Collections.Generic;

namespace Location.Core.Entities
{
    public partial class Country :  BaseEntityModel
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Districts = new HashSet<District>();
            Neighborhoods = new HashSet<Neighborhood>();
            Quarters = new HashSet<Quarter>();
        }
        
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Neighborhood> Neighborhoods { get; set; }
        public virtual ICollection<Quarter> Quarters { get; set; }
    }
}
