namespace Favorite.Api.Extensions;

public static class HostBuilderExtensions
{
    public static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
    {
        var isValidGrpcPort = int.TryParse(config["GRPC_PORT"], out var grpcPort);
        if (!isValidGrpcPort || grpcPort <= 0)
        {
            grpcPort = 5011;
        }

        var isValidPort = int.TryParse(config["PORT"], out var port);
        if (!isValidPort || port <= 0)
        {
            port = 5001;
        }

        return (port, grpcPort);
    }
}