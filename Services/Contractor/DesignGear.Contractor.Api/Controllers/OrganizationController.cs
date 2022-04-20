using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;

namespace DesignGear.Contractor.Api.Controllers
{
    //todo Anton Методы контроллера принимают и возвращают Model вместо Dto.
    //В приложении api должен быть маппинг Model -> Dto и Dto -> Model
    //Т.е. на уровне контроллеров работаем с Model, на уровне сервисов работаем с Dto, на уровне данных работаем с Entity
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [Authorize]
        [HttpPost]
        public Guid CreateOrganization(OrganizationCreateDto organization)
        {
            return _organizationService.CreateOrganization(organization);
        }

        [Authorize]
        [HttpGet("organizationbyuser")]
        public ICollection<OrganizationDto> OrganizationsByUser(Guid userId)
        {
            return _organizationService.GetOrganizationsByUser(userId);
        }
    }
}
