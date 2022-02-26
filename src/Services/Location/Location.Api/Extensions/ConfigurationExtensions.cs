namespace Location.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5013);
        var port = config.GetValue("PORT", 5003);
        return (port, grpcPort);
    }
}