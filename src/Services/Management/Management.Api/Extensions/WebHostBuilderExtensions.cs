using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Management.Api.Extensions;

public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder BuildKestrel(this ConfigureWebHostBuilder builder, IConfiguration configuration)
    {
        builder.ConfigureKestrel(options =>
        {
            var ports = ConfigurationExtensions.TryGetPorts(configuration);
        
            options.Listen(IPAddress.Any, ports.httpPort,
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http1AndHttp2; });
    
            options.Listen(IPAddress.Any, ports.grpcPort,
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
        });

        return builder;
    }
}