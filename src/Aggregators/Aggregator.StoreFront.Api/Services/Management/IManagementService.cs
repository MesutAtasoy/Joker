using Management.Api.Grpc;

namespace Aggregator.StoreFront.Api.Services.Management;

public interface IManagementService
{
    Task<IdNameMessage> GetPricingPlanByIdAsync(Guid id);
}