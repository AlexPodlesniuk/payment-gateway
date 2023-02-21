using PaymentGateway.BuildingBlocks.Application;
using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.Payments.Domain.Payments;

namespace PaymentGateway.Payments.Application.Queries.GetPaymentById;

public record GetPaymentById(string PaymentId) : IQuery<PaymentDetails>;

public record PaymentDetails(
    string PaymentId, 
    string CardNumber, 
    string CardHolderName,
    DateOnly ExpirationDate, 
    PaymentStatus Status);