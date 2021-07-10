using System;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;

namespace Merchant.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommand : IRequest<StoreDto>
    {
        public Guid Id { get; }
        public UpdateStoreDto Store { get; }

        public UpdateStoreCommand(Guid id, UpdateStoreDto store)
        {
            Id = id;
            Store = store;
        }
    }
}