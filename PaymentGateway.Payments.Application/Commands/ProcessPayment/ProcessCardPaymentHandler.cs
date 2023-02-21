using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.BuildingBlocks.Application.Result;
using PaymentGateway.Payments.Application.Providers;
using PaymentGateway.Payments.Domain;
using PaymentGateway.Payments.Domain.Merchants;
using PaymentGateway.Payments.Domain.Payments;
using PaymentGateway.Payments.Domain.ValueObjects;

namespace PaymentGateway.Payments.Application.Commands.ProcessPayment;

internal class ProcessCardPaymentHandler : ICommandHandler<ProcessCardPayment, CardPaymentProcessingResult>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IPaymentProvider _paymentProvider;

    public ProcessCardPaymentHandler(IPaymentRepository paymentRepository, IMerchantRepository merchantRepository, IPaymentProvider paymentProvider)
    {
        _paymentRepository = paymentRepository;
        _merchantRepository = merchantRepository;
        _paymentProvider = paymentProvider;
    }

    public async Task<Result<CardPaymentProcessingResult>> Handle(ProcessCardPayment request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.FindByIdAsync(request.MerchantId, cancellationToken);
        
        if (merchant is null)
            return Result.Fail<CardPaymentProcessingResult>(new Error(nameof(Merchant), "Incorrect merchant"));
        
        var paymentId = await _paymentRepository.AllocateIdentifierAsync(cancellationToken);
        var cardInfo = new Card(new CardNumber(request.BuyerCardNumber), request.BuyerCardHolderName, request.Cvv,
            request.ExpirationDate);
        
        var payment = new Payment(paymentId, PaymentType.Card)
        {
            BuyerId = request.BuyerId,
            Merchant = request.MerchantId,
            Amount = request.Amount,
            Currency = request.Currency,
            BuyerCard = cardInfo
        };

        await _paymentRepository.SaveAsync(payment, cancellationToken);

        var paymentInfo = new PaymentInfo(cardInfo, merchant.BankAccount);
        var processPaymentResult = await _paymentProvider.ProcessPaymentAsync(paymentInfo, cancellationToken);
        
        ProcessPaymentStatus(processPaymentResult, payment);
        
        await _paymentRepository.SaveAsync(payment, cancellationToken);
        
        return Result.Ok(new CardPaymentProcessingResult(paymentId, payment.TransactionStatus));
    }

    private static void ProcessPaymentStatus(Result<string> processPaymentResult, Payment payment)
    {
        if (processPaymentResult.IsSuccess)
            payment.MarkProcessed(processPaymentResult.Value()!);
        else 
            payment.MarkFailed(processPaymentResult.Error!.ErrorMessage);
    }
}