using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace PaymentGateway.BuildingBlocks.Persistence.Mongo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration["MongoConnection:ConnectionString"];
        var databaseName = configuration["MongoConnection:DatabaseName"];

        var mongoClient = new MongoClient(mongoConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);
        services.AddSingleton(mongoDatabase);
        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddScoped(c => 
            c.GetRequiredService<IMongoClient>().StartSession());
        services.AddScoped<ITransaction, Transaction>();

        return services;
    }
}