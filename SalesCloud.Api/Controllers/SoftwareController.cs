using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SalesCloud.Common.Dtos.Ccp;
using SalesCloud.Logic.Contracts;

namespace SalesCloud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;
        private readonly IValidator<PurchaseSoftwareRequest> _validator;

        public SoftwareController(
            ISoftwareService softwareService,
            IValidator<PurchaseSoftwareRequest> validator)
        {
            _softwareService = softwareService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailable()
        {
            var availableSoftware = await _softwareService.GetAvailableSoftware();
            return Ok(availableSoftware);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseSoftware([FromBody] PurchaseSoftwareRequest request)
        {
            _validator.ValidateAndThrow(request);

            var purchasedSoftware = await _softwareService.PurchaseSoftware(request);
            return Ok(purchasedSoftware);
        }
    }
}