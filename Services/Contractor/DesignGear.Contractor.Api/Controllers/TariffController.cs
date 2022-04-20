using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;
        private readonly IMapper _mapper;

        public TariffController(ITariffService tariffService, IMapper mapper)
        {
            _tariffService = tariffService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ICollection<VmTariff>> TariffList()
        {
            return (await _tariffService.GetTariffs()).MapTo<ICollection<VmTariff>>(_mapper);
        }
    }
}
