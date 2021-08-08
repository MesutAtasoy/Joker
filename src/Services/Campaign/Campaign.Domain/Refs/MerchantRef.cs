using System;
using Campaign.Domain.Refs.Base;
using Joker.Exceptions;

namespace Campaign.Domain.Refs
{
    public class MerchantRef : IdNameRef
    {
        public static MerchantRef Create(Guid refId, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            return new MerchantRef
            {
                RefId = refId, 
                Name = name
            };
        }
    }
}