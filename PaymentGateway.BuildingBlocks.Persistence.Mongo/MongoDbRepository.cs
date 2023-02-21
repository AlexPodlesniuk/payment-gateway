using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.BuildingBlocks.Persistence.Mongo;

public class MongoDbRepository<TEntity> : IMongoRepository<TEntity> where TEntity : AggregateRoot
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoDbRepository(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public IMongoQueryable<TEntity> Aggregates => _collection.AsQueryable();
    public ValueTask<string> AllocateIdentifierAsync(CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(Guid.NewGuid().ToString());
    }

    public async Task<TEntity?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
        await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true }, cancellationToken);
    }
}