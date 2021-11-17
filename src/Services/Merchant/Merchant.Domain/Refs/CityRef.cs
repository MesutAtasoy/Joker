using Joker.Exceptions;
using Merchant.Domain.Refs.Base;

namespace Merchant.Domain.Refs;

public class CityRef : IdNameRef
{
    public static CityRef Create(Guid refId, string name)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        return new CityRef
        {
            RefId = refId, 
            Name = name
        };
    }
}