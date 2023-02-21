using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.Acquisition.Domain.Merchant;

public class Merchant : AggregateRoot
{
    public Merchant(string id, string name, string bankAccount) : base(id)
    {
        Name = name;
        BankAccount = bankAccount;
    }

    public string Name { get; init; }
    public string BankAccount { get; private set; }
}