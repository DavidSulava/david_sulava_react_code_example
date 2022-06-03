using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contractor.Core.Services {
    public class AccountService : IAccountService {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IMapper mapper, DataAccessor dataAccessor, IHttpContextAccessor httpContextAccessor) {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AccountDto> GetAccountAsync() {
            var user = (User)_httpContextAccessor.HttpContext.Items["User"];
            return await Task.FromResult(user.MapTo<AccountDto>(_mapper));
        }

        public async Task UpdateAccountAsync(AccountUpdateDto update) {
            var contextUser = (User)_httpContextAccessor.HttpContext.Items["User"];
            var user = await _dataAccessor.Editor.Users.FirstAsync(x => x.Id == contextUser.Id);
            _mapper.Map(update, user);
            await _dataAccessor.Editor.SaveAsync();
        }
    }
}
