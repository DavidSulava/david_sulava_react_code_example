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

        public async Task<Guid> CreateProductAsync(ProductCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var newItem = _mapper.Map<Product>(create);
            _dataAccessor.Editor.Create(newItem);
            await _dataAccessor.Editor.SaveAsync();
            return newItem.Id;
        }

        public async Task UpdateProductAsync(ProductUpdateDto update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var item = await _dataAccessor.Editor.Products.FirstOrDefaultAsync(x => x.Id == update.Id);
            if (item == null)
            {
                throw new EntityNotFoundException<Product>(update.Id);
            }

            _mapper.Map(update, item);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task RemoveProductAsync(Guid id)
        {
            var item = await _dataAccessor.Editor.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            _dataAccessor.Editor.Delete(item);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            var result = await _dataAccessor.Reader.Products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            return result;
        }

        public async Task<TResult> GetProductsByOrganizationAsync<TResult>(Guid organizationId, Func<IQueryable<ProductItemDto>, TResult> resultBuilder)
        {
            if (resultBuilder == null)
            {
                throw new ArgumentNullException(nameof(resultBuilder));
            }
            var query = _dataAccessor.Reader.Products.Where(x => x.OrganizationId == organizationId).ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider);
            var result = resultBuilder(query);
            return await Task.FromResult(result);
        }
    }
}
