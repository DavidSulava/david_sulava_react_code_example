﻿using DesignGear.Contracts.Dto;
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

        [HttpGet()]
        public async Task<ICollection<VmProductVersion>> ProductVersionsByProduct(Guid productId)
        {
            return (await _productVersionService.GetProductVersionsByProductAsync(productId)).MapTo<ICollection<VmProductVersion>>(_mapper);
        }

        [HttpGet("{id}")]
        public async Task<VmProductVersion> ProductVersionById([FromRoute] Guid id)
        {
            return (await _productVersionService.GetProductVersionAsync(id)).MapTo<VmProductVersion>(_mapper);
        }

        [HttpGet]
        [Route("{id}/Model")]
        public async Task<ActionResult> ModelFile([FromRoute] Guid id)
        {
            var modelFile = await _productVersionService.GetModelFileAsync(id);
            if (modelFile == null || modelFile.Content == null)
            {
                return Ok();
            }
            return File(modelFile.Content, "application/octet-stream", modelFile.FileName);
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
