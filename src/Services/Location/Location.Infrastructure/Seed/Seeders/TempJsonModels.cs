using System;
using System.Collections.Generic;
using Location.Core.Entities;

namespace Location.Infrastructure.Seed.Seeders
{
    public class TempJsonModels
    {
        public class TempDistrict : District
        {
            public double[] Location { get; set; }
        }

        public class TempNeighborhood : BaseEntityModel
        {
            public Guid CountryId { get; set; }
            public Guid CityId { get; set; }
            public Guid DistrictId { get; set; }
            public string Name { get; set; }
            public virtual City City { get; set; }
            public virtual Country Country { get; set; }
            public virtual District District { get; set; }
            public virtual ICollection<TempQuarter> Quarters { get; set; }
        }
        
        public class TempQuarter : Quarter
        {
            public double[] Location { get; set; }
        }
    }
}