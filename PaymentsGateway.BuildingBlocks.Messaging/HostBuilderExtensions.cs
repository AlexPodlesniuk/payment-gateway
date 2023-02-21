using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PaymentsGateway.BuildingBlocks.Messaging;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseMessaging(this IHostBuilder hostBuilder, string endpointName)
    {
        hostBuilder.UseNServiceBus(builderContext =>
        {
            var rabbitConnectionString = builderContext.Configuration["RabbitConnection:Host"];
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology(QueueType.Quorum);
            transport.ConnectionString(rabbitConnectionString);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        });
        
        hostBuilder.ConfigureServices(config =>
        {
            config.AddTransient<IIntegrationEventPublisher, NServiceBusEventPublisher>();
        });
        
        return hostBuilder;
    }
}