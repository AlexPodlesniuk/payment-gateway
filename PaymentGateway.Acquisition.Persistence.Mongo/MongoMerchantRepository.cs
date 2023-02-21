using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PaymentGateway.Acquisition.Domain.Merchant;
using PaymentGateway.BuildingBlocks.Persistence.Mongo;

namespace PaymentGateway.Acquisition.Persistence.Mongo;

public class MongoMerchantRepository : MongoDbRepository<Merchant>, IMerchantRepository
{
    public MongoMerchantRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }

    public async Task<Merchant?> FindByNameAndBankAccountAsync(string name, string account, CancellationToken cancellationToken = default)
    {
        return await Aggregates.FirstOrDefaultAsync(x => x.Name == name && x.BankAccount == account, cancellationToken);
    }
}