using Joker.Repositories;
using Management.Core.Entities;

namespace Management.Core.Repositories;

public interface IPaymentMethodRepository : IRepository<PaymentMethod>
{
    Task<List<PaymentMethod>> GetAllActiveAsync();
}