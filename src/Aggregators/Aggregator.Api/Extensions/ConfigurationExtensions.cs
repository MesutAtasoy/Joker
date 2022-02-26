namespace Aggregator.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5016);
        var port = config.GetValue("PORT", 5006);
        return (port, grpcPort);
    }
}