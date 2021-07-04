using System;

namespace Location.Application.Countries.Dto
{
    public class CountryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }
    }
}