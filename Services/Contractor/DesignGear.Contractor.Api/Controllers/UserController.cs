using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /*
         * todo Anton
         * Добавить async в название метода
         */
        [HttpPost]
        public async Task<IActionResult> CreateUser(VmUserCreate user)
        {
            var response = await _userService.CreateUserAsync(user.MapTo<UserCreateDto>(_mapper));

            if(response == Guid.Empty)
                return BadRequest(new { message = "Email already exists" });

            return Ok(response);
        }
    }
}
