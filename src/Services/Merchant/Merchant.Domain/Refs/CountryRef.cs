using System;
using Joker.Exceptions;
using Merchant.Domain.Refs.Base;

namespace Merchant.Domain.Refs
{
    public class CountryRef : IdNameRef
    {
        public static CountryRef Create(Guid refId, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            return new CountryRef
            {
                RefId = refId, 
                Name = name
            };
        }
    }
}