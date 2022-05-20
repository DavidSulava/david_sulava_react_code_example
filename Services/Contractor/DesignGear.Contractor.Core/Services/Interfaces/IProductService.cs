using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto product);

        Task UpdateProductAsync(ProductUpdateDto product);

        Task RemoveProductAsync(Guid id);

        Task<TResult> GetProductsByOrganizationAsync<TResult>(Guid organizationId, Func<IQueryable<ProductDto>, TResult> resultBuilder);

        Task<ProductDto> GetProductAsync(Guid id);
    }
}
