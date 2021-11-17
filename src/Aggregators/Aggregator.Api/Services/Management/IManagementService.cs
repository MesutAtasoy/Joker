using Management.Api.Grpc;

namespace Aggregator.Api.Services.Management;

public interface IManagementService
{
    Task<IdNameMessage> GetBusinessDirectoryByIdAsync(Guid id);
    Task<IdNameMessage> GetPricingPlanByIdAsync(Guid id);
}