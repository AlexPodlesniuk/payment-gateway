using PaymentGateway.Acquisition.Domain.Merchant;
using PaymentGateway.BuildingBlocks.Application.Mediatr;
using PaymentGateway.BuildingBlocks.Application.Result;

namespace PaymentGateway.Acquisition.Application.Queries.GetMerchantById;

internal class GetMerchantByIdHandler : IQueryHandler<GetMerchantById, MerchantDetails>
{
    private readonly IMerchantRepository _merchantRepository;

    public GetMerchantByIdHandler(IMerchantRepository merchantRepository)
    {
        _merchantRepository = merchantRepository;
    }

    public async Task<Result<MerchantDetails>> Handle(GetMerchantById request, CancellationToken cancellationToken)
    {
        var merchant = await _merchantRepository.FindByIdAsync(request.MerchantId, cancellationToken);
        
        if (merchant is null)
            return Result.Fail<MerchantDetails>(new ErrorNotFound(nameof(Merchant)));
        
        return Result.Ok(new MerchantDetails(merchant.Name, merchant.BankAccount));
    }
}