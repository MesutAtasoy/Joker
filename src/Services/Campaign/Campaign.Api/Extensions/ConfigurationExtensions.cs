namespace Campaign.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5011);
        var port = config.GetValue("PORT", 5001);
        return (port, grpcPort);
    }
}