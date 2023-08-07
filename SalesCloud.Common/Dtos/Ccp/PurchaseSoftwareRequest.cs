namespace SalesCloud.Common.Dtos.Ccp
{
    public record PurchaseSoftwareRequest(Guid AccountId, Guid ProviderSoftwareId, int Quantity);
}