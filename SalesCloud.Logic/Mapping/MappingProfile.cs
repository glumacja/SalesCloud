using AutoMapper;
using SalesCloud.Common.Dtos;
using SalesCloud.Domain.Models;

namespace SalesCloud.Logic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();

            CreateMap<PurchasedSoftware, PurchasedSoftwareDto>();
        }
    }
}