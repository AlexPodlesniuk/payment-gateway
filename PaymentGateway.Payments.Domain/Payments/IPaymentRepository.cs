using PaymentGateway.BuildingBlocks.Domain;

namespace PaymentGateway.Payments.Domain.Payments;

public interface IPaymentRepository : IRepository<Payment>
{
    
}