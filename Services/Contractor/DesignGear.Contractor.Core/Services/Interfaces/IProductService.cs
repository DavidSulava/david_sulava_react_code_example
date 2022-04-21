using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProduct(ProductCreateDto product);

        Task UpdateProduct(ProductUpdateDto product);

        Task RemoveProduct(Guid productId);

        Task<ICollection<ProductDto>> GetProductsByOrganization(Guid organizationId);

        Task<ProductDto> GetProductAsync(Guid productId);
    }
}
