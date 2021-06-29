using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Domain.Refs;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, StoreListDto>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public CreateStoreCommandHandler(IMerchantRepository merchantRepository, 
            IStoreRepository storeRepository, 
            IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreListDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetByIdAsync(request.MerchantId);

            if (merchant == null)
            {
                throw new NotFoundException("Merchant is not found");
            }

            var merchantRef = MerchantRef.Create(merchant.Id, merchant.Name);

            var storeId = IdGenerationFactory.Create();

            var storeLocation = new StoreLocation(
                CountryRef.Create(request.Location.Country.RefId, request.Location.Country.Name),
                CityRef.Create(request.Location.City.RefId, request.Location.City.Name),
                NeighborhoodRef.Create(request.Location.Neighborhood.RefId, request.Location.Neighborhood.Name),
                DistrictRef.Create(request.Location.District.RefId, request.Location.District.Name),
                QuarterRef.Create(request.Location.Quarter.RefId, request.Location.Quarter.Name)
            );
            
            var store = Store.Create(storeId,
                merchantRef,
                request.Name,
                request.Slogan,
                request.PhoneNumber,
                request.Email,
                request.Description,
                storeLocation
            );

            await _storeRepository.AddAsync(store);
            
            return _mapper.Map<StoreListDto>(store);
        }
    }
}