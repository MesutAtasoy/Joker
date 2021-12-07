using AutoMapper;
using AutoMapper.QueryableExtensions;
using Joker.Extensions;
using Joker.Extensions.Models;
using MediatR;
using Merchant.Application.Services;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Queries.GetStores;

public class GetStoresQueryHandler : IRequestHandler<GetStoresQuery, PagedList<StoreDto>>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetStoresQueryHandler(IStoreRepository storeRepository,
        IMapper mapper,
        IUserService userService)
    {
        _storeRepository = storeRepository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<PagedList<StoreDto>> Handle(GetStoresQuery request,
        CancellationToken cancellationToken)
    {
        var organizationId = _userService.GetOrganizationId();
        
        var stores = _storeRepository.Get()
            .Where(x => !x.IsDeleted && x.OrganizationId == organizationId)
            .ProjectTo<StoreDto>(_mapper.ConfigurationProvider)
            .ToPagedList(request.Filter);

        return stores;
    }
}