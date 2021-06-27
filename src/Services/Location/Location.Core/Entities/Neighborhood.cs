using System;
using System.Collections.Generic;

namespace Location.Core.Entities
{
    public partial class Neighborhood : BaseEntityModel
    {
        public Neighborhood()
        {
            Quarters = new HashSet<Quarter>();
        }
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Name { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Quarter> Quarters { get; set; }
    }
}
