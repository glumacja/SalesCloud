using SalesCloud.Domain.Models;

namespace SalesCloud.Data.Contracts
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdWithAccounts(Guid customerId);
    }
}