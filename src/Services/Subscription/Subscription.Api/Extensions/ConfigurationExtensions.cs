namespace Subscription.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5017);
        var port = config.GetValue("PORT", 5007);
        return (port, grpcPort);
    }
}