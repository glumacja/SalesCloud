using SalesCloud.Data.Contracts;
using SalesCloud.Infrastructure.Context;

namespace SalesCloud.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository(ApplicationContext context)
        {
        }
    }
}