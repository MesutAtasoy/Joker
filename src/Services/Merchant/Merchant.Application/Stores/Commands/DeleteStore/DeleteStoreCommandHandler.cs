using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Merchant.Application.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, bool>
    {
        private readonly StoreManager _storeManager;
        public DeleteStoreCommandHandler(StoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        public async Task<bool> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            return await _storeManager.DeleteAsync(request);
        }
    }
}