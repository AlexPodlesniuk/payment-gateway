using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.BuildingBlocks.Application.Behaviors;
using PaymentGateway.Payments.Application.Options;
using PaymentGateway.Payments.Application.Providers;

namespace PaymentGateway.Payments.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<PaymentProviderOptions>(configuration.GetSection(PaymentProviderOptions.SectionName));
        serviceCollection.AddHttpClient<PaymentProvider>();
        serviceCollection.AddTransient<IPaymentProvider, PaymentProvider>();
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