using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Models.Contractor;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    //todo Anton Методы контроллера принимают и возвращают Model вместо Dto.
    //В приложении api должен быть маппинг Model -> Dto и Dto -> Model
    //Т.е. на уровне контроллеров работаем с Model, на уровне сервисов работаем с Dto, на уровне данных работаем с Entity
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //todo Anton тут можно указать сваггеру какие типы (AuthenticateResponseModel) может возвращать метод,
        //чтобы сваггер это задокументировал и фронтдендер видел эту информацию. Вроде есть како-йто атрибут для этого
        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequestModel model)
        {
            var response = _authenticationService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
