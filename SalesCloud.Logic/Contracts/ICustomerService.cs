using SalesCloud.Common.Dtos;

namespace SalesCloud.Logic.Contracts
{
    public interface ICustomerService
    {
        Task<List<AccountDto>> GetAccounts(Guid customerId);
    }
}