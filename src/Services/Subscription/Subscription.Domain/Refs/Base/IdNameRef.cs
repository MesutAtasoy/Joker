using System;

namespace Subscription.Domain.Refs.Base
{
    public abstract class IdNameRef 
    {
        public Guid RefId { get; protected init; }
        public string Name { get; protected init; }
    }
}