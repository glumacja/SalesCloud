using SalesCloud.Common.Dtos.Ccp;

namespace SalesCloud.Logic.Contracts.Integrations
{
    public interface ICcpService
    {
        Task<List<AvailableSoftwareResponse>> GetAvailableSoftware();

        Task<PurchaseSoftwareResponse> PurchaseSoftware(PurchaseSoftwareRequest request);

        Task CancelLicense(CancelLicenseRequest request);

        Task ExtendLicense(ExtendLicenseRequest request);

        Task UpdateLicenseQuantity(UpdateLicenseQuantityRequest request);
    }
}