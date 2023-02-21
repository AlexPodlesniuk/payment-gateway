using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PaymentGateway.BuildingBlocks.Api.Abstractions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
    {
        var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        host.ConfigureAppConfiguration((ctx, builder) =>
        {
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
            builder.AddEnvironmentVariables();
        });

        return host;
    }
}

