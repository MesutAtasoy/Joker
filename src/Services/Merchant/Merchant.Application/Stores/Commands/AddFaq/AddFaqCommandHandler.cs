using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Joker.Exceptions;
using MediatR;
using Merchant.Application.Stores.Dto;
using Merchant.Domain.StoreAggregate;
using Merchant.Domain.StoreAggregate.Repositories;
using Merchant.Infrastructure.Factories;

namespace Merchant.Application.Stores.Commands.AddFaq
{
    public class AddFaqCommandHandler : IRequestHandler<AddFaqCommand, StoreFAQDto>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public AddFaqCommandHandler(IStoreRepository storeRepository,
            IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreFAQDto> Handle(AddFaqCommand request, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null)
            {
                throw new NotFoundException("Store is not found");
            }

            var faqId = IdGenerationFactory.Create();
            
            var faq = new StoreFAQ(faqId, request.Faq.Question, request.Faq.Answer, request.Faq.Order);
            
            store.AddFAQ(faq);

            await _storeRepository.UpdateAsync(store.Id, store);

            return _mapper.Map<StoreFAQDto>(faq);
        }
    }
}