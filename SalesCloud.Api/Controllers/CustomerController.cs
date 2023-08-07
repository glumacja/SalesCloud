using Microsoft.AspNetCore.Mvc;
using SalesCloud.Logic.Contracts;

namespace SalesCloud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("{customerId:guid}/accounts")]
        public async Task<IActionResult> GetAccounts(Guid customerId)
        {
            var accounts = await _customerService.GetAccounts(customerId);

            return Ok(accounts);
        }
    }
}