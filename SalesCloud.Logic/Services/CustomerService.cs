using SalesCloud.Common.Dtos;
using SalesCloud.Data.Contracts;
using SalesCloud.Domain.Exceptions;
using SalesCloud.Logic.Contracts;

namespace SalesCloud.Logic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CustomerService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<AccountDto>> GetAccounts(Guid customerId)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdWithAccounts(customerId)
                ?? throw new NotFoundException("Customer does not exist");

            return customer.Accounts.Select(x => new AccountDto(x.Id, x.Name)).ToList();
        }
    }
}