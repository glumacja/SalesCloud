using FluentValidation;
using SalesCloud.Common.Dtos.Ccp;

namespace SalesCloud.Logic.Validators
{
    public class UpdateLicenseQuantityValidator : AbstractValidator<UpdateLicenseQuantityRequest>
    {
        public UpdateLicenseQuantityValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required");
        }
    }
}