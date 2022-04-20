using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    //todo Anton Методы контроллера принимают и возвращают Model вместо Dto.
    //В приложении api должен быть маппинг Model -> Dto и Dto -> Model
    //Т.е. на уровне контроллеров работаем с Model, на уровне сервисов работаем с Dto, на уровне данных работаем с Entity
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        [Authorize]
        [HttpGet]
        public ICollection<TariffDto> TariffList()
        {
            return _tariffService.GetTariffs();
        }
    }
}
