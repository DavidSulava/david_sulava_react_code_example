using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto product);

        Task UpdateProductAsync(ProductUpdateDto product);

        Task RemoveProductAsync(Guid id);

        Task<ICollection<ProductDto>> GetProductsByOrganizationAsync(Guid organizationId);

        Task<ProductDto> GetProductAsync(Guid id);
    }
}
