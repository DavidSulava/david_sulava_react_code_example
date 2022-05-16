using AutoMapper;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.Contracts.Models.ConfigManager;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ConfigManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;
        public ModelController(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task SetModelAsync([FromForm] VmModel model) {
            //parse model file
            //create Configuration record
            //create ParameterDefinition records
        }
    }
}
