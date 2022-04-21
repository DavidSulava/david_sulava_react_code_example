using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Common.Exceptions;

namespace DesignGear.Contractor.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public ProductService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public async Task<Guid> CreateProduct(ProductCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var newProduct = _mapper.Map<Product>(create);
            _dataAccessor.Editor.Create(newProduct);
            await _dataAccessor.Editor.SaveAsync();
            return newProduct.Id;
        }

        public async Task UpdateProduct(ProductUpdateDto update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var product = await _dataAccessor.Editor.Products.FirstOrDefaultAsync(x => x.Id == update.Id);
            if (product == null)
            {
                throw new EntityNotFoundException<Product>(update.Id);
            }

            _mapper.Map(update, product);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task RemoveProduct(Guid id)
        {
            var product = await _dataAccessor.Editor.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            _dataAccessor.Editor.Delete(product);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task<ICollection<ProductDto>> GetProductsByOrganization(Guid organizationId)
        {
            return await _dataAccessor.Reader.Products.Where(x => x.OrganizationId == organizationId).
                ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductDto> GetProductAsync(Guid productId)
        {
            var product = await _dataAccessor.Reader.Products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new EntityNotFoundException<Product>(productId);
            }

            return product;
        }
    }
}
