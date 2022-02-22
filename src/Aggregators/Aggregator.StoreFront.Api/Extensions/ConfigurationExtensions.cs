namespace Aggregator.StoreFront.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
         var grpcPort = config.GetValue("GRPC_PORT", 5040);
         var port = config.GetValue("PORT", 5020);
         return (port, grpcPort);
    }
}