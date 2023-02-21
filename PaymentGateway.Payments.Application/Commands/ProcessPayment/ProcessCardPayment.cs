using PaymentGateway.BuildingBlocks.Application;
using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.Payments.Domain.Payments;

namespace PaymentGateway.Payments.Application.Commands.ProcessPayment;

public record ProcessCardPayment(
    string BuyerId, 
    string MerchantId, 
    decimal Amount, 
    string Currency, 
    string BuyerCardNumber, 
    string BuyerCardHolderName,
    string Cvv,
    DateOnly ExpirationDate) : ICommand<CardPaymentProcessingResult>;

public record CardPaymentProcessingResult(string PaymentId, PaymentStatus Status);