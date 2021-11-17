using AutoMapper;
using Joker.Exceptions;
using Merchant.Application.Stores.Commands.CreateStore;
using Merchant.Application.Stores.Commands.DeleteStore;
using Merchant.Application.Stores.Commands.UpdateLocation;
using Merchant.Application.Stores.Commands.UpdateStore;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.MerchantAggregate.Repositories;
using Merchant.Domain.Refs;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Stores;

public class StoreManager
{
    private readonly IStoreRepository _storeRepository;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IMapper _mapper;

    public StoreManager(IStoreRepository storeRepository,
        IMerchantRepository merchantRepository,
        IMapper mapper)
    {
        _storeRepository = storeRepository;
        _mapper = mapper;
        _merchantRepository = merchantRepository;
    }

    public async Task<StoreDto> CreateAsync(CreateStoreCommand request)
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
            QuarterRef.Create(request.Location.Quarter.RefId, request.Location.Quarter.Name),
            request.Location.Address
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

        return _mapper.Map<StoreDto>(store);
    }

    public async Task<StoreDto> UpdateAsync(UpdateStoreCommand request)
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

        return _mapper.Map<StoreDto>(store);
    }

    public async Task<bool> DeleteAsync(DeleteStoreCommand request)
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

    public async Task<StoreLocationDto> UpdateLocationAsync(UpdateLocationCommand request)
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
            QuarterRef.Create(request.Location.Quarter.RefId, request.Location.Quarter.Name),
            request.Location.Address
        );

        store.UpdateLocation(storeLocation);

        await _storeRepository.UpdateAsync(store.Id, store);

        return _mapper.Map<StoreLocationDto>(storeLocation);
    }

    public async Task<StoreDto> GetByIdAsync(Guid id)
    {
        var store = await _storeRepository.GetByIdAsync(id);
        if (store == null)
        {
            throw new NotFoundException("Store is not found");
        }

        return _mapper.Map<StoreDto>(store);
    }
}