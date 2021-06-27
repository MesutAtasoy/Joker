using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;

namespace Merchant.Application.Stores.Commands.AddBusinessHour
{
    public class AddBusinessHourCommandHandler : IRequestHandler<AddBusinessHourCommand, StoreBusinessHourDto>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        public AddBusinessHourCommandHandler(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        public async Task<StoreBusinessHourDto> Handle(AddBusinessHourCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }

            var businessHour = new StoreBusinessHour(request.BusinessHour.DayOfWeek,
                request.BusinessHour.StartTime,
                request.BusinessHour.EndTime,
                request.BusinessHour.IsTwentyFourHour);
            
            store.AddBusinessHour(businessHour);

            await _storeRepository.UpdateAsync(store.Id, store);

            return _mapper.Map<StoreBusinessHourDto>(businessHour);
        }
    }
}