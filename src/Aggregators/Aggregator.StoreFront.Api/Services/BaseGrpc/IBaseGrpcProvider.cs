using Grpc.Core;

namespace Aggregator.StoreFront.Api.Services.BaseGrpc;

public interface IBaseGrpcProvider
{
    Task<Metadata> GetDefaultHeadersAsync();
}