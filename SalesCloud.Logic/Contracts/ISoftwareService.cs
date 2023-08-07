using SalesCloud.Common.Dtos;
using SalesCloud.Common.Dtos.Ccp;

namespace SalesCloud.Logic.Contracts
{
    public interface ISoftwareService
    {
        Task<List<AvailableSoftwareResponse>> GetAvailableSoftware();

        Task<PurchasedSoftwareDto> PurchaseSoftware(PurchaseSoftwareRequest request);

        Task CancelLicense(Guid licenseId);

        Task ExtendLicense(Guid licenseId, ExtendLicenseRequest request);

        Task UpdateLicenseQuantity(Guid licenseId, UpdateLicenseQuantityRequest request);
    }
}