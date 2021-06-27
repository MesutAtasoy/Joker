using System.Collections.Generic;
using Management.Core.Entities;
using MediatR;

namespace Management.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQuery : IRequest<List<Currency>>
    {
        
    }
}