using SalesCloud.Common.Consts;
using SalesCloud.Common.Dtos.Ccp;
using SalesCloud.Domain.Exceptions;
using SalesCloud.Logic.Contracts.Integrations;

namespace SalesCloud.Logic.Services.Integrations
{
    public class CcpService : ICcpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CcpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<AvailableSoftwareResponse>> GetAvailableSoftware()
        {
            // just example of use of IHttpClientFactory
            var ccpClient = _httpClientFactory.CreateClient("CcpApiClient");

            List<AvailableSoftwareResponse> listOfSoftware = new()
            {
                new AvailableSoftwareResponse(SoftwareProductIds.Office, nameof(SoftwareProductIds.Office)),
                new AvailableSoftwareResponse(SoftwareProductIds.Windows, nameof(SoftwareProductIds.Windows)),
                new AvailableSoftwareResponse(SoftwareProductIds.VisualStudio, nameof(SoftwareProductIds.VisualStudio))
            };

            return await Task.FromResult(listOfSoftware!);
        }

        public Task<PurchaseSoftwareResponse> PurchaseSoftware(PurchaseSoftwareRequest request)
        {
            return request.ProviderSoftwareId switch
            {
                Guid g when g == SoftwareProductIds.Office =>
                    Task.FromResult(new PurchaseSoftwareResponse(SoftwareProductIds.Office, nameof(SoftwareProductIds.Office), request.Quantity, request.Quantity)),
                Guid g when g == SoftwareProductIds.Windows =>
                    Task.FromResult(new PurchaseSoftwareResponse(SoftwareProductIds.Windows, nameof(SoftwareProductIds.Windows), request.Quantity, request.Quantity)),
                Guid g when g == SoftwareProductIds.VisualStudio =>
                    Task.FromResult(new PurchaseSoftwareResponse(SoftwareProductIds.VisualStudio, nameof(SoftwareProductIds.VisualStudio), request.Quantity, request.Quantity)),
                _ => throw new NotFoundException("Software not found")
            };
        }

        public async Task CancelLicense(CancelLicenseRequest request) => await Task.CompletedTask;

        public async Task ExtendLicense(ExtendLicenseRequest request) => await Task.CompletedTask;

        public async Task UpdateLicenseQuantity(UpdateLicenseQuantityRequest request) => await Task.CompletedTask;
    }
}