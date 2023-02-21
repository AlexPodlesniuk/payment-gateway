using PaymentGateway.Payments.Domain.ValueObjects;

namespace PaymentGateway.Payments.Domain;

public record Card(CardNumber CardNumber, string CardHolder, string Cvv, DateOnly ExpirationDate);