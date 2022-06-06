using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;

namespace DesignGear.Contractor.Api.Mapping {
    public class AccountMapping : Profile {
        public AccountMapping() {
            CreateMap<AccountDto, VmAccount>(MemberList.None);
            CreateMap<VmAccountUpdate, AccountUpdateDto>(MemberList.None);
        }
    }
}
