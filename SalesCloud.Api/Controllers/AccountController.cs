using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SalesCloud.Common.Dtos.Ccp;
using SalesCloud.Logic.Contracts;

namespace SalesCloud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISoftwareService _softwareService;
        private readonly IValidator<ExtendLicenseRequest> _extendRequestValidator;
        private readonly IValidator<UpdateLicenseQuantityRequest> _updateQuantityValidator;

        public AccountController(
            IAccountService accountService,
            ISoftwareService softwareService,
            IValidator<UpdateLicenseQuantityRequest> updateQuantityValidator,
            IValidator<ExtendLicenseRequest> extendLicenseValidator)
        {
            _accountService = accountService;
            _softwareService = softwareService;
            _updateQuantityValidator = updateQuantityValidator;
            _extendRequestValidator = extendLicenseValidator;
        }

        [HttpGet]
        [Route("{accountId:guid}/licenses")]
        public async Task<IActionResult> GetPurchasedSoftware(Guid accountId)
        {
            var purchasedLicenses = _accountService.GetPurchasedSoftware(accountId);
            return Ok(purchasedLicenses);
        }

        [HttpPost]
        [Route("{accountId:guid}/licenses/{licenseId:guid}/cancel")]
        public async Task<IActionResult> PurchaseLicense(Guid licenseId)
        {
            await _softwareService.CancelLicense(licenseId);

            return Ok();
        }

        [HttpPost]
        [Route("{accountId:guid}/licenses/{licenseId:guid}/extend")]
        public async Task<IActionResult> ExtendLicense(Guid licenseId, [FromBody] ExtendLicenseRequest licenseParams)
        {
            await _extendRequestValidator.ValidateAndThrowAsync(licenseParams);

            await _softwareService.ExtendLicense(licenseId, licenseParams);

            return Ok();
        }

        [HttpPost]
        [Route("{accountId:guid}/licenses/{licenseId:guid}/updateQuantity")]
        public async Task<IActionResult> UpdateLicenseQuantity(Guid licenseId, [FromBody] UpdateLicenseQuantityRequest licenseParams)
        {
            await _updateQuantityValidator.ValidateAndThrowAsync(licenseParams);

            await _softwareService.UpdateLicenseQuantity(licenseId, licenseParams);

            return Ok();
        }
    }
}