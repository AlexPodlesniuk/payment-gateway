namespace PaymentGateway.BuildingBlocks.Persistence;

public interface ITransaction : IDisposable
{
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task AbortTransactionAsync(CancellationToken cancellationToken);
}