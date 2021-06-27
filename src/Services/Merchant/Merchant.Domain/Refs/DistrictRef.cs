using System;
using Joker.Exceptions;
using Merchant.Domain.Refs.Base;

namespace Merchant.Domain.Refs
{
    public class DistrictRef : IdNameRef
    {
        public static DistrictRef Create(Guid refId, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            return new DistrictRef
            {
                RefId = refId, 
                Name = name
            };
        }
    }
}