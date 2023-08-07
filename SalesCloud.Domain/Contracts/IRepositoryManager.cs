namespace SalesCloud.Data.Contracts
{
    public interface IRepositoryManager
    {
        ICustomerRepository CustomerRepository { get; }
        IPurchasedSoftwareRepository PurchasedSoftwareRepository { get; }
        IAccountRepository AccountRepository { get; }

        Task SaveAsync();
    }
}