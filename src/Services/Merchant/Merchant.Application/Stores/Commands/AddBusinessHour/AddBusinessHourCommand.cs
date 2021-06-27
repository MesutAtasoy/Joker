using System;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;

namespace Merchant.Application.Stores.Commands.AddBusinessHour
{
    public class AddBusinessHourCommand : IRequest<StoreBusinessHourDto>
    {
        public AddBusinessHourCommand(Guid storeId, AddBusinessHourDto businessHour)
        {
            StoreId = storeId;
            BusinessHour = businessHour;
        }

        public Guid StoreId { get; }
        public AddBusinessHourDto BusinessHour { get; }
    }
}