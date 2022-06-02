using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Models.Contractor;
using DesignGear.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Data.Entity;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        //todo тут можно указать сваггеру какие типы (AuthenticateResponseModel) может возвращать метод,
        //чтобы сваггер это задокументировал и фронтдендер видел эту информацию. Вроде есть како-йто атрибут для этого
        [HttpPost]
        public async Task<IActionResult> AuthenticateAsync(VmAuthenticateRequest model)
        {
            var response = await _authenticationService.AuthenticateAsync(model.MapTo<AuthenticateRequestDto>(_mapper));

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response.MapTo<VmAuthenticateResponse>(_mapper));
        }

        [Authorize]
        [HttpPost("organization")]
        public async Task<IActionResult> SetOrganizationAsync(Guid organizationId)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _authenticationService.SetOrganizationAsync(user.Id, organizationId);

            return Ok(response.MapTo<VmAuthenticateResponse>(_mapper));
        }

        [HttpPost("PasswordRecoveryRequest")]
        public async Task SendPasswordRecoveryRequestAsync(VmPasswordRecoveryRequest request) {
            await _authenticationService.SendPasswordRecoveryRequestAsync(request.MapTo<PasswordRecoveryRequestDto>(_mapper));
        }

        [HttpPost("RecoverPassword")]
        public async Task RecoverPasswordAsync(VmPasswordRecoveryCommit commit) {
            await _authenticationService.ChangePasswordAsync(commit.MapTo<PasswordRecoveryCommitDto>(_mapper));
        }

    }
}
