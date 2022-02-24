namespace Merchant.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5010);
        var port = config.GetValue("PORT", 5000);
        return (port, grpcPort);
    }
}