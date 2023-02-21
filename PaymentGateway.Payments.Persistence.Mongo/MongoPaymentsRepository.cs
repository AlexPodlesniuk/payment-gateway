using MongoDB.Driver;
using PaymentGateway.BuildingBlocks.Persistence.Mongo;
using PaymentGateway.Payments.Domain.Payments;

namespace PaymentGateway.Payments.Persistence.Mongo;

public class MongoPaymentsRepository : MongoDbRepository<Payment>, IPaymentRepository
{
    public MongoPaymentsRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}