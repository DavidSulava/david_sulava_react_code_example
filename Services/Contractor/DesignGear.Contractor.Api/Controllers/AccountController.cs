using AutoMapper;
using DesignGear.Common.Enums;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers {
    [Authorize(Policy = "OrganizationSelected")]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper) {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<VmAccount> GetAccountInfoAsync() {
            return (await _accountService.GetAccountAsync()).MapTo<VmAccount>(_mapper);
        }

        [HttpPut]
        public async Task UpdateAccountAsync(VmAccountUpdate update) {
            await _accountService.UpdateAccountAsync(update.MapTo<AccountUpdateDto>(_mapper));
        }
    }
}
