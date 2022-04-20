using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult CreateUser(UserCreateDto user)
        {
            var response = _userService.CreateUser(user);

            if(response == Guid.Empty)
                return BadRequest(new { message = "Email already exists" });

            return Ok(response);
        }
    }
}
