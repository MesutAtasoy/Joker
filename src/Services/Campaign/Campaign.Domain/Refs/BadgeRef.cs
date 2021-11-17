using Campaign.Domain.Refs.Base;
using Joker.Exceptions;

namespace Campaign.Domain.Refs;

public class BadgeRef : IdNameRef
{
    public static BadgeRef Create(Guid refId, string name)
    {
        Check.NotNullOrEmpty(name, nameof(name));

        return new BadgeRef
        {
            RefId = refId,
            Name = name
        };
    }
}