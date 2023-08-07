using AutoMapper;
using SalesCloud.Common.Dtos;
using SalesCloud.Data.Contracts;
using SalesCloud.Logic.Contracts;

namespace SalesCloud.Logic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AccountService(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public List<PurchasedSoftwareDto> GetPurchasedSoftware(Guid accountId)
        {
            var purchasedLicenses = _repositoryManager.PurchasedSoftwareRepository.GetByCondition(ps => ps.AccountId.Equals(accountId));

            return _mapper.Map<List<PurchasedSoftwareDto>>(purchasedLicenses);
        }
    }
}