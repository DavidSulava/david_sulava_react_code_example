using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contractor.Core.Mapping {
    public class AccountMapping : Profile {
        public AccountMapping() {
            CreateMap<User, AccountDto>(MemberList.None);
            CreateMap<AccountUpdateDto, User>(MemberList.None);
        }
    }
}
