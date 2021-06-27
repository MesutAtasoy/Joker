using System.Threading;
using System.Threading.Tasks;
using Joker.Exceptions;
using MediatR;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Commands.RemoveBusinessHour
{
    public class RemoveBusinessHourCommandHandler : IRequestHandler<RemoveBusinessHourCommand, bool>
    {
        private readonly IStoreRepository _storeRepository;

        public RemoveBusinessHourCommandHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<bool> Handle(RemoveBusinessHourCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }

            store.RemoveBusinessHour(request.DayOfWeek);

            await _storeRepository.UpdateAsync(store.Id, store);
            
            return true;
        }
    }
}