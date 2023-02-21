using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.Payments.Domain.Merchants;

public class Merchant : AggregateRoot
{
    public Merchant(string id, string name, string account) : base(id)
    {
        Name = name;
        BankAccount = account;
    }

    public string Name { get; init; }
    public string BankAccount { get; private set; }
}