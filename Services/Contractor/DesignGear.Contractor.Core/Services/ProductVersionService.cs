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
    public class ProductVersionService : IProductVersionService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public ProductVersionService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public async Task<Guid> CreateProductVersionAsync(ProductVersionCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var newItem = _mapper.Map<ProductVersion>(create);
            _dataAccessor.Editor.Create(newItem);
            await _dataAccessor.Editor.SaveAsync();
            return newItem.Id;
        }

        public async Task UpdateProductVersionAsync(ProductVersionUpdateDto update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var item = await _dataAccessor.Editor.ProductVersions.FirstOrDefaultAsync(x => x.Id == update.Id);
            if (item == null)
            {
                throw new EntityNotFoundException<ProductVersion>(update.Id);
            }

            _mapper.Map(update, item);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task RemoveProductVersionAsync(Guid id)
        {
            var item = await _dataAccessor.Editor.ProductVersions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                throw new EntityNotFoundException<ProductVersion>(id);
            }

            _dataAccessor.Editor.Delete(item);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task<ICollection<ProductVersionDto>> GetProductVersionsByProductAsync(Guid productId)
        {
            return await _dataAccessor.Reader.ProductVersions.Where(x => x.ProductId == productId).
                ProjectTo<ProductVersionDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductVersionDto> GetProductVersionAsync(Guid id)
        {
            var result = await _dataAccessor.Reader.ProductVersions.ProjectTo<ProductVersionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException<ProductVersion>(id);
            }

            return result;
        }
    }
}
