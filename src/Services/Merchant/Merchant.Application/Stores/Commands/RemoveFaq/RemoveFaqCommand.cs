using System;
using MediatR;

namespace Merchant.Application.Stores.Commands.RemoveFaq
{
    public class RemoveFaqCommand : IRequest<bool>
    {
        public RemoveFaqCommand(Guid storeId, Guid faqId)
        {
            StoreId = storeId;
            FaqId = faqId;
        }

        public Guid StoreId { get; }
        public Guid FaqId { get; }
    }
}