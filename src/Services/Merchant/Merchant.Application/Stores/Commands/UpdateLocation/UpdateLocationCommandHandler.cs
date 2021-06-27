using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.Refs;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, StoreLocationDto>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        
        public UpdateLocationCommandHandler(IStoreRepository storeRepository, 
            IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        public async Task<StoreLocationDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }  
            
            var storeLocation = new StoreLocation(
                CountryRef.Create(request.Location.Country.RefId, request.Location.Country.Name),
                CityRef.Create(request.Location.City.RefId, request.Location.City.Name),
                NeighborhoodRef.Create(request.Location.Neighborhood.RefId, request.Location.Neighborhood.Name),
                DistrictRef.Create(request.Location.District.RefId, request.Location.District.Name),
                QuarterRef.Create(request.Location.Quarter.RefId, request.Location.Quarter.Name)
            );
            
            store.UpdateLocation(storeLocation);

            await _storeRepository.UpdateAsync(store.Id, store);

            return _mapper.Map<StoreLocationDto>(storeLocation);
        }
    }
}