using FluentValidation;
using SalesCloud.Common.Dtos.Ccp;

namespace SalesCloud.Logic.Validators
{
    public class PurchaseSoftwareRequestValidator : AbstractValidator<PurchaseSoftwareRequest>
    {
        public PurchaseSoftwareRequestValidator()
        {
            RuleFor(x => x.ProviderSoftwareId)
                .NotEmpty()
                .WithMessage("ProviderSoftwareId is required");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required");

            RuleFor(x => x.AccountId)
                .NotEmpty()
                .WithMessage("AccountId is required");
        }
    }
}