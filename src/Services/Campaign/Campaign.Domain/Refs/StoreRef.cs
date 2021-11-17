using Campaign.Domain.Refs.Base;
using Joker.Exceptions;

namespace Campaign.Domain.Refs;

public class StoreRef : IdNameRef
{
    public static StoreRef Create(Guid refId, string name)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        return new StoreRef
        {
            RefId = refId, 
            Name = name
        };
    }
}