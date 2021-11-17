using Joker.Exceptions;
using Merchant.Domain.Refs.Base;

namespace Merchant.Domain.Refs;

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