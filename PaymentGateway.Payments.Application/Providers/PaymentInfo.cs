using PaymentGateway.Payments.Domain;

namespace PaymentGateway.Payments.Application.Providers;

public record PaymentInfo(Card BuyerCard, string MerchantBankAccount);