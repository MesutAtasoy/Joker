using System;
using MediatR;

namespace Merchant.Application.Stores.Commands.RemoveBusinessHour
{
    public class RemoveBusinessHourCommand : IRequest<bool>
    {
        public RemoveBusinessHourCommand(Guid storeId, int dayOfWeek)
        {
            StoreId = storeId;
            DayOfWeek = dayOfWeek;
        }

        public Guid StoreId { get; }
        public int DayOfWeek { get; }
    }
}