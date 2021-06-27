using System;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;

namespace Merchant.Application.Stores.Commands.AddFaq
{
    public class AddFaqCommand : IRequest<StoreFAQDto>
    {
        public AddFaqCommand(Guid storeId, AddFaqDto faq)
        {
            StoreId = storeId;
            Faq = faq;
        }

        public Guid StoreId { get; }
        public AddFaqDto Faq { get; }
    }
}