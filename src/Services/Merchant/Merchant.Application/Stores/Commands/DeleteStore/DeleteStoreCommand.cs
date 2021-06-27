using System;
using MediatR;

namespace Merchant.Application.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommand : IRequest<bool>
    {
        public DeleteStoreCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}