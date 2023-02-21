using MongoDB.Driver;

namespace PaymentGateway.BuildingBlocks.Persistence.Mongo;

public class Transaction : ITransaction
{
    private readonly IClientSessionHandle _sessionHandle;

    public Transaction(IClientSessionHandle sessionHandle)
    {
        _sessionHandle = sessionHandle;
        _sessionHandle.StartTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await _sessionHandle.CommitTransactionAsync(cancellationToken);
    }

    public async Task AbortTransactionAsync(CancellationToken cancellationToken)
    {
        await _sessionHandle.AbortTransactionAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _sessionHandle.Dispose();
    }
}