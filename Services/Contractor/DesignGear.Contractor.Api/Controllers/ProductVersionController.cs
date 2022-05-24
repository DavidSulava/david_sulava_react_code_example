using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Models.Contractor;
using AutoMapper;
using DesignGear.Common.Extensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductVersionController : ControllerBase
    {
        private readonly IProductVersionService _productVersionService;
        private readonly IMapper _mapper;

        public ProductVersionController(IProductVersionService productVersionService, IMapper mapper)
        {
            _productVersionService = productVersionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Guid> CreateProductVersionAsync([FromForm] VmProductVersionCreate create)
        {
            return await _productVersionService.CreateProductVersionAsync(create.MapTo<ProductVersionCreateDto>(_mapper));
        }

        [HttpPut]
        public async Task UpdateProductVersionAsync([FromForm] VmProductVersionUpdate update)
        {
            await _productVersionService.UpdateProductVersionAsync(update.MapTo<ProductVersionUpdateDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveProductVersionAsync(Guid id)
        {
            await _productVersionService.RemoveProductVersionAsync(id);
        }

        [HttpGet]
        public async Task<DataSourceResult> GetProductVersionItemsAsync(Guid productId, [DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            return await _productVersionService.GetProductVersionsByProductAsync(productId, query => query.ToDataSourceResult(dataSourceRequest, _mapper.Map<ProductVersionItemDto, VmProductVersionItem>));
        }

        [HttpGet("{id}")]
        public async Task<VmProductVersion> ProductVersionByIdAsync([FromRoute] Guid id)
        {
            return (await _productVersionService.GetProductVersionAsync(id)).MapTo<VmProductVersion>(_mapper);
        }

        [HttpGet]
        [Route("{id}/Images/{fileName}")]
        public async Task<ActionResult> Images([FromRoute] Guid id, [FromRoute] string fileName)
        {
            var image = await _productVersionService.GetImageFileAsync(id, fileName);
            if (image == null || image.Content == null)
            {
                return Ok();
            }
            return File(image.Content, image.ContentType, image.FileName);
        }

    }
}
