namespace Bank.Api.Models;

public record PaymentInfo(Card BuyerCard, string MerchantBankAccount);

public record Card(CardNumber CardNumber, string CardHolder, string Cvv, DateOnly ExpirationDate);

public record CardNumber(string Number);