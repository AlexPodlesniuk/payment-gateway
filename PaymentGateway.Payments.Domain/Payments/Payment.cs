using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.Payments.Domain.Payments;

public class Payment : AggregateRoot
{
    public Payment(string id, PaymentType paymentType) : base(id)
    {
        TransactionStatus = PaymentStatus.Pending;
        PaymentType = paymentType;
    }

    public PaymentStatus TransactionStatus { get; private set; }
    public PaymentType PaymentType { get; init; }
    public string BuyerId { get; init; }
    public string Merchant { get; init; }
    public string Currency { get; init; }
    public decimal Amount { get; init; }
    public Card BuyerCard { get; init; }
    public string TransactionDetails { get; private set; }
    
    public void MarkProcessed(string details)
    {
        TransactionStatus = PaymentStatus.Success;
        TransactionDetails = details;
    }

    public void MarkFailed(string details)
    {
        TransactionStatus = PaymentStatus.Failed;
        TransactionDetails = details;
    }
}