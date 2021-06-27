using System;
using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<StoreLocationDto>
    {
        public UpdateLocationCommand(Guid storeId, StoreLocationDto location)
        {
            StoreId = storeId;
            Location = location;
        }

        public Guid StoreId { get;  }
        public StoreLocationDto Location { get; }
    }
}