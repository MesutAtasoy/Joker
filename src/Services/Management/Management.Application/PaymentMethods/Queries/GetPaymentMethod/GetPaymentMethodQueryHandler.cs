using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.PaymentMethods.Queries.GetPaymentMethod
{
    public class GetPaymentMethodQueryHandler : IRequestHandler<GetPaymentMethodQuery, List<PaymentMethod>>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public GetPaymentMethodQueryHandler(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<List<PaymentMethod>> Handle(GetPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            return await _paymentMethodRepository.GetAllActiveAsync();
        }
    }
}