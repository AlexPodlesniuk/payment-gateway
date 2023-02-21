using MongoDB.Driver;
using PaymentGateway.BuildingBlocks.Persistence.Mongo;
using PaymentGateway.Payments.Domain.Merchants;

namespace PaymentGateway.Payments.Persistence.Mongo;

public class MongoMerchantRepository : MongoDbRepository<Merchant>, IMerchantRepository
{
    public MongoMerchantRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}