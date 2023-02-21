using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.BuildingBlocks.Persistence.Mongo;
using PaymentGateway.Payments.Domain.Merchants;
using PaymentGateway.Payments.Domain.Payments;

namespace PaymentGateway.Payments.Persistence.Mongo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbPersistence(configuration);
        services.AddTransient<IPaymentRepository, MongoPaymentsRepository>();
        services.AddTransient<IMerchantRepository, MongoMerchantRepository>();
        
        return services;
    }
}