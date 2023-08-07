using SalesCloud.Common.Dtos;

namespace SalesCloud.Logic.Contracts
{
    public interface IAccountService
    {
        List<PurchasedSoftwareDto> GetPurchasedSoftware(Guid accountId);
    }
}