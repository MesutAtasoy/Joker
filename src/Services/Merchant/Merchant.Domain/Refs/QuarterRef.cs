using Joker.Exceptions;
using Merchant.Domain.Refs.Base;

namespace Merchant.Domain.Refs;

public class QuarterRef : IdNameRef
{
    public static QuarterRef Create(Guid refId, string name)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        return new QuarterRef
        {
            RefId = refId, 
            Name = name
        };
    }
}