using Management.Core.Entities;
using MediatR;

namespace Management.Application.PaymentMethods.Queries.GetPaymentMethod;

public class GetPaymentMethodQuery : IRequest<List<PaymentMethod>>
{
}