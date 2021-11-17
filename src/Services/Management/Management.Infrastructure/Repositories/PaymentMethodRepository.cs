using Joker.EntityFrameworkCore.Repositories;
using Management.Core.Entities;
using Management.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories;

public class PaymentMethodRepository : EntityFrameworkCoreRepository<ManagementContext, PaymentMethod>,
    IPaymentMethodRepository
{
    public PaymentMethodRepository(ManagementContext context)
        : base(context)
    {
    }

    public async Task<List<PaymentMethod>> GetAllActiveAsync()
    {
        return await base.Get(x => !x.IsDeleted)
            .ToListAsync();
    }
}