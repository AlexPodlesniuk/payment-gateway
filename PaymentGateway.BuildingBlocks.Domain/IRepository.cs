namespace PaymentGateway.BuildingBlocks.Domain;

public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    ValueTask<string> AllocateIdentifierAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
}