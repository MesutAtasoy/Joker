using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Merchant.Application.Stores.Dto;

namespace Merchant.Application.Stores.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, StoreLocationDto>
    {
        private readonly StoreManager _storeManager;
        
        public UpdateLocationCommandHandler(StoreManager storeManager)
        {
            _storeManager = storeManager;
        }
        public async Task<StoreLocationDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            return await _storeManager.UpdateLocationAsync(request);
        }
    }
}