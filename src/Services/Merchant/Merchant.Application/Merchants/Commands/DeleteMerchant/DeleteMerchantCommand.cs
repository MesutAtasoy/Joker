using System;
using MediatR;

namespace Merchant.Application.Merchants.Commands.DeleteMerchant
{
    public class DeleteMerchantCommand : IRequest<bool>
    {
        public DeleteMerchantCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}