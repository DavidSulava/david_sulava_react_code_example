using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Models;
using AutoMapper;
using DesignGear.Common.Extensions;

namespace DesignGear.Contractor.Api.Controllers
{
    /* todo Anton 
     * На мой взгляд лучше переименовать ConfigurationController в ModelController, с удалением лишних методов
     * В бизнес-логике есть понятие "модель". Несмотря на то, что это по сути конфигурация, на уровне
     * интерфейса лучше разделить эти два понятия, т.к. условно завтра модель может быть уже не конфигурацией, а чем то другим
     * 
     * ConfigurationController у нас также будет, но будет отвечать именно за конфигурации (точнее будет выступать 
     * в роли прокси к ConfigurationManager)
     */

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;
        private readonly IMapper _mapper;

        public ConfigurationController(IConfigurationService configurationService, IMapper mapper)
        {
            _configurationService = configurationService;
            _mapper = mapper;
        }

        /*
         * todo Anton добавить async в название метода
         */
        [HttpPost]
        public async Task<Guid> CreateConfiguration([FromForm] VmConfigurationCreate create)
        {
            return await _configurationService.CreateConfigurationAsync(create.MapTo<ConfigurationCreateDto>(_mapper));
        }

        /*
        * todo Anton добавить async в название метода
        */
        [HttpPut]
        public async Task UpdateConfiguration([FromForm] VmConfigurationUpdate update)
        {
            await _configurationService.UpdateConfigurationAsync(update.MapTo<ConfigurationUpdateDto>(_mapper));
        }

        /*
        * todo Anton добавить async в название метода
        */
        [HttpDelete]
        public async Task RemoveConfiguration(Guid id)
        {
            await _configurationService.RemoveConfigurationAsync(id);
        }

        /*
        * todo Anton Метод лучше назвать GetConfigurationItemsAsync
        * В первую очередь данный метод предназначен для возврата списка конфигураций для отображения в гриде,
        * поэтому не нужно возвращать полную информацию о конфигурации, например не нужно возвращать ParameterDefinitions.
        * Тут лучше определить отдельную модель, например VmConfigurationItem и возвращать ее
        */
        [HttpGet]
        public async Task<ICollection<VmConfiguration>> ConfigurationsByProduct(Guid productVersionId)
        {
            return (await _configurationService.GetConfigurationsByProductVersionAsync(productVersionId)).MapTo<ICollection<VmConfiguration>>(_mapper);
        }

        /*
        * todo Anton добавить async в название метода
        * Метод лучше назвать GetConfigurationAsync
        */
        [HttpGet("{id}")]
        public async Task<VmConfiguration> ConfigurationById([FromRoute] Guid id)
        {
            return (await _configurationService.GetConfigurationAsync(id)).MapTo<VmConfiguration>(_mapper);
        }

        /*
        * todo Anton добавить async в название метода
        * Метод лучше назвать GetModelFileAsync
        */
        [HttpGet]
        [Route("{id}/Model")]
        public async Task<ActionResult> ModelFile([FromRoute] Guid id)
        {
            var modelFile = await _configurationService.GetModelFileAsync(id);
            if (modelFile == null || modelFile.Content == null)
            {
                return Ok();
            }
            return File(modelFile.Content, modelFile.ContentType, modelFile.FileName);
        }
    }
}
