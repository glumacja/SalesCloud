using AutoMapper;
using SalesCloud.Common.Dtos;
using SalesCloud.Common.Dtos.Ccp;
using SalesCloud.Data.Contracts;
using SalesCloud.Domain.Enums;
using SalesCloud.Domain.Exceptions;
using SalesCloud.Domain.Models;
using SalesCloud.Logic.Contracts;
using SalesCloud.Logic.Contracts.Integrations;

namespace SalesCloud.Logic.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly ICcpService _ccpService;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SoftwareService(
            ICcpService ccpService,
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _ccpService = ccpService;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<List<AvailableSoftwareResponse>> GetAvailableSoftware()
        {
            return await _ccpService.GetAvailableSoftware();
        }

        public async Task<PurchasedSoftwareDto> PurchaseSoftware(PurchaseSoftwareRequest request)
        {
            ThrowIfLicenseForAccountExists(request);

            var ccpResponse = await _ccpService.PurchaseSoftware(request) ?? throw new Exception("Ccp response invalid");

            var entity = CreatePurchasedSoftwareEntity(ccpResponse, request);
            _repositoryManager.PurchasedSoftwareRepository.AddEntity(entity);

            await _repositoryManager.SaveAsync();

            return _mapper.Map<PurchasedSoftwareDto>(entity);
        }

        public async Task CancelLicense(Guid licenseId)
        {
            var license = GetLicenseByIdThrowIfNull(licenseId);

            license.State = PurchasedSoftwareState.Cancelled;
            _repositoryManager.PurchasedSoftwareRepository.UpdateEntity(license);

            await _ccpService.CancelLicense(new CancelLicenseRequest());

            await _repositoryManager.SaveAsync();
        }

        public async Task ExtendLicense(Guid licenseId, ExtendLicenseRequest request)
        {
            var license = GetLicenseByIdThrowIfNull(licenseId);

            license.ValidTo = license.ValidTo.AddMonths(request.ExtensionPeriodInMonths);
            _repositoryManager.PurchasedSoftwareRepository.UpdateEntity(license);

            await _ccpService.ExtendLicense(request);

            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateLicenseQuantity(Guid licenseId, UpdateLicenseQuantityRequest request)
        {
            var license = GetLicenseByIdThrowIfNull(licenseId);

            license.Quantity += request.Quantity;
            _repositoryManager.PurchasedSoftwareRepository.UpdateEntity(license);

            await _ccpService.UpdateLicenseQuantity(request);

            await _repositoryManager.SaveAsync();
        }

        private PurchasedSoftware GetLicenseByIdThrowIfNull(Guid licenseId)
        {
            return _repositoryManager.PurchasedSoftwareRepository.GetById(licenseId) ??
                throw new Exception("License does not exist");
        }

        private void ThrowIfLicenseForAccountExists(PurchaseSoftwareRequest request)
        {
            var license = _repositoryManager.PurchasedSoftwareRepository.GetByCondition(
                ps =>
                    ps.AccountId.Equals(request.AccountId) &&
                    ps.ExternalProviderSoftwareId.Equals(request.ProviderSoftwareId));

            if (license.Count != 0)
            {
                throw new BadRequestException("There is existing license for provided account");
            }
        }

        private static PurchasedSoftware CreatePurchasedSoftwareEntity(PurchaseSoftwareResponse ccpResponse, PurchaseSoftwareRequest request)
            => new()
            {
                Id = Guid.NewGuid(),
                State = PurchasedSoftwareState.Active,
                ExternalProviderSoftwareId = ccpResponse.Id,
                AccountId = request.AccountId,
                Name = ccpResponse.Name,
                Quantity = ccpResponse.Quantity,
                ValidTo = DateTime.UtcNow.AddMonths(ccpResponse.LicenseDurationInMonths)
            };
    }
}