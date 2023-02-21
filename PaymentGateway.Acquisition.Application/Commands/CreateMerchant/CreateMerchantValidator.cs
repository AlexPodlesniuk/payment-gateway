using FluentValidation;

namespace PaymentGateway.Acquisition.Application.Commands.CreateMerchant;

public class CreateMerchantValidator : AbstractValidator<CreateMerchant>
{
    public CreateMerchantValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} should not be empty");
        
        RuleFor(x => x.BankAccount)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BankAccount)} should not be empty");
    }
}