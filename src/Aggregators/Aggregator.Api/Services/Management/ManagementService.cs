using System;
using System.Threading.Tasks;
using Management.Api.Grpc;
using IdName = Campaign.Api.Grpc.IdName;

namespace Aggregator.Api.Services.Management
{
    public class ManagementService : IManagementService
    {
        private readonly ManagementApiGrpcService.ManagementApiGrpcServiceClient _managementApiGrpcServiceClient;
        
        public ManagementService(ManagementApiGrpcService.ManagementApiGrpcServiceClient managementApiGrpcServiceClient)
        {
            _managementApiGrpcServiceClient = managementApiGrpcServiceClient;
        }
        
        public async Task<IdName> GetBusinessDirectoryByIdAsync(Guid id)
        {
            var response = await _managementApiGrpcServiceClient.GetBusinessDirectoryByIdAsync(new ByIdMessage {Id = id.ToString()});
            
            return new IdName
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        public async Task<IdName> GetPricingPlanByIdAsync(Guid id)
        {
            var response = await _managementApiGrpcServiceClient.GetPricingPlanByIdAsync(new ByIdMessage {Id = id.ToString()});
            return new IdName
            {
                Id = response.Id,
                Name = response.Name
            };
        }
    }
}