﻿using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IProductVersionService
    {
        Task<Guid> CreateProductVersionAsync(ProductVersionCreateDto productVersion);

        Task UpdateProductVersionAsync(ProductVersionUpdateDto productVersion);

        Task RemoveProductVersionAsync(Guid id);

        Task<TResult> GetProductVersionsByProductAsync<TResult>(Guid productId, Func<IQueryable<ProductVersionItemDto>, TResult> resultBuilder);

        Task<ProductVersionDto> GetProductVersionAsync(Guid id);

        Task<ProductVersionPreviewDto> GetPreviewImageAsync(Guid id, string fileName);
    }
}
