using FluentValidation;

namespace PaymentGateway.Payments.Application.Commands.ProcessPayment;

internal class ProcessCardPaymentValidator : AbstractValidator<ProcessCardPayment>
{
    public ProcessCardPaymentValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Amount)} should be greater than 0");

        RuleFor(x => x.BuyerId)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BuyerId)} should not be empty");
        
        RuleFor(x => x.MerchantId)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.MerchantId)} should not be empty");
        
        RuleFor(x => x.BuyerCardHolderName)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BuyerCardHolderName)} should not be empty");
        
        RuleFor(x => x.BuyerCardNumber)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.BuyerCardNumber)} should not be empty");
        
        RuleFor(x => x.ExpirationDate)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.ExpirationDate)} should not be empty");
        
        RuleFor(x => x.Cvv)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Cvv)} should not be empty");
        
        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Currency)} should not be empty");
        
        RuleFor(x => x.Cvv)
            .Matches("^[0-9]{3,4}$")
            .WithMessage(x => $"{nameof(x.Cvv)} should not be valid CVV");

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage(x => $"{nameof(x.ExpirationDate)} should be in the future");
        
        RuleFor(x => x.BuyerCardNumber)
            .CreditCard()
            .WithMessage(x => $"{x.BuyerCardNumber} is not a valid bank card number");
    }
}