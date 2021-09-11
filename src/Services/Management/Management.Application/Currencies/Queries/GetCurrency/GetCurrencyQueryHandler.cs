using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var currencies = await _currencyRepository.Get(x => !x.IsDeleted)
                .ToListAsync(cancellationToken);
            
            return currencies;
        }
    }
}