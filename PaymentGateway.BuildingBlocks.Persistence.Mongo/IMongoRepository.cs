using MongoDB.Driver.Linq;
using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.BuildingBlocks.Persistence.Mongo;

public interface IMongoRepository<TEntity> : IRepository<TEntity>
where TEntity : AggregateRoot
{
    IMongoQueryable<TEntity> Aggregates { get; }
}