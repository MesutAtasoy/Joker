namespace Favorite.Api.Extensions;

public static class ConfigurationExtensions
{
    public static (int httpPort, int grpcPort) TryGetPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5015);
        var port = config.GetValue("PORT", 5005);
        return (port, grpcPort);
    }
}