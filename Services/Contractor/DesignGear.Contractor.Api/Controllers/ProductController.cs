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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Guid> CreateProduct(VmProductCreate product)
        {
            return await _productService.CreateProductAsync(product.MapTo<ProductCreateDto>(_mapper));
        }

        [HttpPut]
        public async Task UpdateProduct(VmProductUpdate product)
        {
            await _productService.UpdateProductAsync(product.MapTo<ProductUpdateDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveProduct(Guid productId)
        {
            await _productService.RemoveProductAsync(productId);
        }

        [HttpGet("byorganization")]
        public async Task<ICollection<VmProduct>> ProductsByOrganization(Guid organizationId)
        {
            return (await _productService.GetProductsByOrganizationAsync(organizationId)).MapTo<ICollection<VmProduct>>(_mapper);
        }

        [HttpGet("{id}")]
        public async Task<VmProduct> ProductById([FromRoute] Guid id)
        {
            return (await _productService.GetProductAsync(id)).MapTo<VmProduct>(_mapper);
        }
    }
}
