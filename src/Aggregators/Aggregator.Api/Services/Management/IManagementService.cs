using System;
using System.Threading.Tasks;
using Campaign.Api.Grpc;

namespace Aggregator.Api.Services.Management
{
    public interface IManagementService
    {
        Task<IdName> GetBusinessDirectoryByIdAsync(Guid id);
    }
}