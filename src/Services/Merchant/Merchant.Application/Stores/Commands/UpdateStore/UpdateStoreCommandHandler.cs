using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, StoreListDto>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        
        public UpdateStoreCommandHandler(IStoreRepository storeRepository,
            IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        public async Task<StoreListDto> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.Id);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }
            
            store.Update(request.Store.Name,
                request.Store.Slogan,
                request.Store.PhoneNumber,
                request.Store.Email,
                request.Store.Description);

            await _storeRepository.UpdateAsync(store.Id, store);

            return _mapper.Map<StoreListDto>(store);
        }
    }
}