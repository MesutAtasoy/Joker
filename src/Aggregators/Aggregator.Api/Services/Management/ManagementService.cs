using Management.Api.Grpc;

namespace Aggregator.Api.Services.Management;

public class ManagementService : IManagementService
{
    private readonly ManagementApiGrpcService.ManagementApiGrpcServiceClient _managementApiGrpcServiceClient;
        
    public ManagementService(ManagementApiGrpcService.ManagementApiGrpcServiceClient managementApiGrpcServiceClient)
    {
        _managementApiGrpcServiceClient = managementApiGrpcServiceClient;
    }
        
    public async Task<IdNameMessage> GetBusinessDirectoryByIdAsync(Guid id)
    {
        return await _managementApiGrpcServiceClient.GetBusinessDirectoryByIdAsync(new ByIdMessage {Id = id.ToString()});
    }

    public async Task<IdNameMessage> GetPricingPlanByIdAsync(Guid id)
    {
        return await _managementApiGrpcServiceClient.GetPricingPlanByIdAsync(new ByIdMessage {Id = id.ToString()});
    }
}