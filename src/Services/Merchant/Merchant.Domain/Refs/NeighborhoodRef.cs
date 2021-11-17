using Joker.Exceptions;
using Merchant.Domain.Refs.Base;

namespace Merchant.Domain.Refs;

public class NeighborhoodRef : IdNameRef
{
    public static NeighborhoodRef Create(Guid refId, string name)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        return new NeighborhoodRef
        {
            RefId = refId, 
            Name = name
        };
    }
}