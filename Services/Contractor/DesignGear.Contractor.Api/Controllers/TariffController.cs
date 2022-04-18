using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        [HttpGet]
        public ICollection<TariffDto> TariffList()
        {
            return _tariffService.GetTariffs();
        }
    }
}
