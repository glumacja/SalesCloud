using SalesCloud.Data.Contracts;
using SalesCloud.Infrastructure.Context;

namespace SalesCloud.Infrastructure.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _dbContext;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<IPurchasedSoftwareRepository> _purchasedSoftwareRepository;

        public RepositoryManager(ApplicationContext context)
        {
            _dbContext = context;
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(context));
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(context));
            _purchasedSoftwareRepository = new Lazy<IPurchasedSoftwareRepository>(() => new PurchasedSoftwareRepository(context));
        }

        public ICustomerRepository CustomerRepository => _customerRepository.Value;

        public IPurchasedSoftwareRepository PurchasedSoftwareRepository => _purchasedSoftwareRepository.Value;

        public IAccountRepository AccountRepository => _accountRepository.Value;

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}