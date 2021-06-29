using System;
using Campaign.Domain.Refs.Base;
using Joker.Exceptions;

namespace Campaign.Domain.Refs
{
    public class BusinessDirectoryRef : IdNameRef
    {
        public static BusinessDirectoryRef Create(Guid refId, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            return new BusinessDirectoryRef
            {
                RefId = refId, 
                Name = name
            };
        }
    }
}