using Grpc.Core;

namespace Aggregator.Api.Services.BaseGrpc;

public interface IBaseGrpcProvider
{
    Task<Metadata> GetDefaultHeadersAsync();
}