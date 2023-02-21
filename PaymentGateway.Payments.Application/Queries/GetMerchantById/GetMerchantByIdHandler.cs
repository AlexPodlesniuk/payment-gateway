using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.BuildingBlocks.Application.Result;
using PaymentGateway.Payments.Domain.Merchants;

namespace PaymentGateway.Payments.Application.Queries.GetMerchantById;

internal class GetMerchantByIdHandler : IQueryHandler<GetMerchantById, MerchantDetails?>
{
    private readonly IMerchantRepository _merchantRepository;

    public GetMerchantByIdHandler(IMerchantRepository merchantRepository)
    {
        _merchantRepository = merchantRepository;
    }

    public async Task<Result<MerchantDetails?>> Handle(GetMerchantById request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.FindByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
            return Result.Fail<MerchantDetails?>(new Error("Merchant", "Merchant with provided id not found"));
        
        return Result.Ok<MerchantDetails?>(new MerchantDetails(merchant.Name));
    }
}