using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.BuildingBlocks.Application.Result;
using PaymentGateway.Payments.Domain.Payments;

namespace PaymentGateway.Payments.Application.Queries.GetPaymentById;

internal class GetPaymentByIdHandler : IQueryHandler<GetPaymentById, PaymentDetails>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByIdHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<PaymentDetails>> Handle(GetPaymentById request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.FindByIdAsync(request.PaymentId, cancellationToken);

        if (payment is null)
            return Result.Fail<PaymentDetails>(new ErrorNotFound(nameof(Payment)));

        return Result.Ok(new PaymentDetails(
            request.PaymentId,
            payment.BuyerCard.CardNumber.Obfuscate(),
            payment.BuyerCard.CardHolder,
            payment.BuyerCard.ExpirationDate,
            payment.TransactionStatus));
    }
}