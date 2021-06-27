using System;
using System.Threading;
using System.Threading.Tasks;
using Joker.Exceptions;
using MediatR;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Guid>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IStoreRepository _storeRepository;

        public CreateStoreCommandHandler(IMerchantRepository merchantRepository, 
            IStoreRepository storeRepository)
        {
            _merchantRepository = merchantRepository;
            _storeRepository = storeRepository;
        }

        public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetByIdAsync(request.MerchantId);

            if (merchant == null)
            {
                throw new NotFoundException("Merchant is not found");
            }

            var storeId = IdGenerationFactory.Create();
            
            var store = Store.Create(storeId,
                merchant.Id,
                request.Name,
                request.Slogan,
                request.PhoneNumber,
                request.Email,
                request.Description
            );

            await _storeRepository.AddAsync(store);
            
            return storeId;
        }
    }
}