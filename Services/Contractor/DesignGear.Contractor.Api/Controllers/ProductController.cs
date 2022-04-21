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

        [Authorize]
        [HttpPost]
        public async Task<Guid> CreateProduct(VmProductCreate product)
        {
            return await _productService.CreateProduct(product.MapTo<ProductCreateDto>(_mapper));
        }

        [Authorize]
        [HttpPost("update")]
        public async Task UpdateProduct(VmProductUpdate product)
        {
            await _productService.UpdateProduct(product.MapTo<ProductUpdateDto>(_mapper));
        }

        [Authorize]
        [HttpPost("remove")]
        public async Task RemoveProduct(Guid productId)
        {
            await _productService.RemoveProduct(productId);
        }

        [Authorize]
        [HttpGet("byorganization")]
        public async Task<ICollection<VmProduct>> ProductsByOrganization(Guid organizationId)
        {
            return (await _productService.GetProductsByOrganization(organizationId)).MapTo<ICollection<VmProduct>>(_mapper);
        }

        [Authorize]
        [HttpGet]
        public async Task<VmProduct> ProductsById(Guid productId)
        {
            return (await _productService.GetProductAsync(productId)).MapTo<VmProduct>(_mapper);
        }
    }
}
