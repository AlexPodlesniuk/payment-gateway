using PaymentGateway.Payments.Application.Commands.ProcessPayment;

namespace PaymentGateway.Payments.Api.Models;

public record CardPaymentRequest(string BuyerId, string MerchantId, CardPaymentDetails CardPayment)
{
    public ProcessCardPayment ToProcessPaymentCommand()
    {
        return new ProcessCardPayment(
            BuyerId,
            MerchantId,
            CardPayment.Amount,
            CardPayment.Currency,
            CardPayment.BuyerCardDetails.CardNumber,
            CardPayment.BuyerCardDetails.CardHolderName,
            CardPayment.BuyerCardDetails.Cvv,
            CardPayment.BuyerCardDetails.ExpirationDate);
    }
}
public record CardPaymentDetails(decimal Amount, string Currency, CardDetails BuyerCardDetails);
public record CardDetails(string CardNumber, string CardHolderName, DateOnly ExpirationDate, string Cvv);