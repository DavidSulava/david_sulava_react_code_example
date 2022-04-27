using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Models;
using AutoMapper;
using DesignGear.Common.Extensions;

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
        public async Task<Guid> CreateProductVersion([FromForm] VmProductVersionCreate create)
        {
            return await _productVersionService.CreateProductVersionAsync(create.MapTo<ProductVersionCreateDto>(_mapper));
        }

        [HttpPut]
        public async Task UpdateProductVersion([FromForm] VmProductVersionUpdate update)
        {
            await _productVersionService.UpdateProductVersionAsync(update.MapTo<ProductVersionUpdateDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveProductVersion(Guid id)
        {
            await _productVersionService.RemoveProductVersionAsync(id);
        }

        [HttpGet("byproduct")]
        public async Task<ICollection<VmProductVersion>> ProductVersionsByProduct(Guid productId)
        {
            return (await _productVersionService.GetProductVersionsByProductAsync(productId)).MapTo<ICollection<VmProductVersion>>(_mapper);
        }

        [HttpGet("{id}")]
        public async Task<VmProductVersion> ProductVersionById([FromRoute] Guid id)
        {
            return (await _productVersionService.GetProductVersionAsync(id)).MapTo<VmProductVersion>(_mapper);
        }

        [HttpGet("model")]
        public async Task<VmAttachment> ModelFile(Guid id)
        {
            return (await _productVersionService.GetModelFileAsync(id)).MapTo<VmAttachment>(_mapper);
        }

        [HttpGet("images")]
        public async Task<ICollection<VmAttachment>> Images(Guid id)
        {
            return (await _productVersionService.GetImageFilesAsync(id)).MapTo<ICollection<VmAttachment>>(_mapper);
        }

    }
}
