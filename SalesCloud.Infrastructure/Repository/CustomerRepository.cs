using Microsoft.EntityFrameworkCore;
using SalesCloud.Data.Contracts;
using SalesCloud.Domain.Models;
using SalesCloud.Infrastructure.Context;

namespace SalesCloud.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _dbContext;

        public CustomerRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        public async Task<Customer?> GetByIdWithAccounts(Guid customerId)
        {
            return await _dbContext.Customers.Include(x => x.Accounts).SingleOrDefaultAsync(c => c.Id.Equals(customerId));
        }
    }
}