using FluentValidation;
using SalesCloud.Common.Dtos.Ccp;

namespace SalesCloud.Logic.Validators
{
    public class ExtendLicenseRequestValidator : AbstractValidator<ExtendLicenseRequest>
    {
        public ExtendLicenseRequestValidator()
        {
            RuleFor(x => x.ExtensionPeriodInMonths)
                .NotEmpty()
                .WithMessage("Extension period is required");
        }
    }
}