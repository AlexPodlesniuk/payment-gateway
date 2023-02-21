using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.BuildingBlocks.Application.Behaviors;

namespace PaymentGateway.Acquisition.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        return serviceCollection;
    }
}