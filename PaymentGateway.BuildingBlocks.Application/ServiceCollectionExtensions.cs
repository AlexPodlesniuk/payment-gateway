using Microsoft.Extensions.DependencyInjection;

namespace PaymentGateway.BuildingBlocks.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBuildingBlockApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}