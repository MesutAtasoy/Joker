using Grpc.Core;
using Grpc.Core.Interceptors;
using Joker.Exceptions;

namespace Aggregator.StoreFront.Api.Interceptors;

public class GrpcExceptionInterceptor : Interceptor
{
    private readonly ILogger<GrpcExceptionInterceptor> _logger;

    public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var call = continuation(request, context);

        return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
    }

    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t)
    {
        try
        {
            var response = await t;
            return response;
        }
        catch (RpcException e)
        {
            var metadata = e.Trailers;
            _logger.LogError("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
            throw new JokerException(metadata.GetValue("errormessage"), 400);
        }
    }
}