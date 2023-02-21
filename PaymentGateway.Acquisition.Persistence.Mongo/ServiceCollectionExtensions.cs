using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Acquisition.Domain.Merchant;
using PaymentGateway.BuildingBlocks.Persistence.Mongo;

namespace PaymentGateway.Acquisition.Persistence.Mongo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbPersistence(configuration);
        services.AddTransient<IMerchantRepository, MongoMerchantRepository>();
        
        return services;
    }
}