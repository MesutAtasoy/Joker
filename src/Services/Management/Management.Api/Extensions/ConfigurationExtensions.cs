namespace Management.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5012);
        var port = config.GetValue("PORT", 5002);
        return (port, grpcPort);
    }
}