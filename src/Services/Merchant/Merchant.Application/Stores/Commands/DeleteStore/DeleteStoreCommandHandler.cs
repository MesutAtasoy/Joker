using System.Threading;
using System.Threading.Tasks;
using Joker.Exceptions;
using MediatR;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, bool>
    {
        private readonly IStoreRepository _storeRepository;

        public DeleteStoreCommandHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<bool> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.Id);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }
            
            store.MarkAsDeleted();

            await _storeRepository.UpdateAsync(store.Id, store);
            
            return true;
        }
    }
}