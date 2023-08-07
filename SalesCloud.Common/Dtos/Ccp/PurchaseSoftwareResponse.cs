namespace SalesCloud.Common.Dtos.Ccp
{
    public record PurchaseSoftwareResponse(Guid Id, string Name, int Quantity, int LicenseDurationInMonths);
}