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
    [Authorize(Policy = "OrganizationSelected")]
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
        public async Task<Guid> CreateProductAsync(VmProductCreate create)
        {
            var product = create.MapTo<ProductCreateDto>(_mapper);
            product.OrganizationId = (Guid)HttpContext.Items["OrganizationId"];
            return await _productService.CreateProductAsync(product);
        }

        [HttpPut]
        public async Task UpdateProductAsync(VmProductUpdate product)
        {
            await _productService.UpdateProductAsync(product.MapTo<ProductUpdateDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveProductAsync(Guid productId)
        {
            await _productService.RemoveProductAsync(productId);
        }

        [HttpGet]
        public async Task<DataSourceResult> GetProductItemsAsync([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            Guid organizationId = (Guid)HttpContext.Items["OrganizationId"];
            return await _productService.GetProductsByOrganizationAsync(organizationId, query => query.ToDataSourceResult(dataSourceRequest, _mapper.Map<ProductItemDto, VmProductItem>));
        }

        [HttpGet("{id}")]
        public async Task<VmProduct> GetProductAsync([FromRoute] Guid id)
        {
            return (await _productService.GetProductAsync(id)).MapTo<VmProduct>(_mapper);
        }
    }
}
