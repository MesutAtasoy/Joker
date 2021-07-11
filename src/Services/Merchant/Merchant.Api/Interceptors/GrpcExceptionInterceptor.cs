using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Joker.Exceptions;
using Microsoft.Extensions.Logging;

namespace Merchant.Api.Interceptors
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly ILogger<GrpcExceptionInterceptor> _logger;

        public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var httpContext = context.GetHttpContext();
            _logger.LogDebug($"Starting call. Request: {httpContext?.Request?.Path}");
            
            try
            {
                return await continuation(request, context);
            }
            
            catch (Exception e)
            {
                var metadata = new Metadata {{"ErrorMessage", e.Message}};
                _logger.LogError(e, $"An error occured when calling {context.Method}");
                throw new RpcException(Status.DefaultCancelled, metadata );
            }
        }
    }
}