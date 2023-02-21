namespace PaymentGateway.BuildingBlocks.Domain;

public abstract class AggregateRoot
{
    public string Id;

    protected AggregateRoot(string id)
    {
        Id = id;
    }
}