using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, List<Currency>>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetCurrencyQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<Currency>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            var currencies = await Task.FromResult(_currencyRepository.Get(x => !x.IsDeleted)
                .ToList());
            
            return currencies;
        }
    }
}