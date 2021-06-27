using System.Threading;
using System.Threading.Tasks;
using Joker.Exceptions;
using MediatR;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Commands.RemoveFaq
{
    public class RemoveFaqCommandHandler : IRequestHandler<RemoveFaqCommand, bool>
    {
        private readonly IStoreRepository _storeRepository;
        
        public RemoveFaqCommandHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        
        public async Task<bool> Handle(RemoveFaqCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }

            store.RemoveFAQ(request.FaqId);

            await _storeRepository.UpdateAsync(store.Id, store);
            
            return true;
        }
    }
}