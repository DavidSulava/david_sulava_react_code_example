using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IProductVersionService
    {
        Task<Guid> CreateProductVersionAsync(ProductVersionCreateDto productVersion);

        Task UpdateProductVersionAsync(ProductVersionUpdateDto productVersion);

        Task RemoveProductVersionAsync(Guid id);

        Task<ICollection<ProductVersionDto>> GetProductVersionsByProductAsync(Guid productId);

        Task<TResult> GetProductVersionsByProductKendoAsync<TResult>(Guid organizationId, Func<IQueryable<ProductVersionDto>, TResult> resultBuilder);

        Task<ProductVersionDto> GetProductVersionAsync(Guid id);

        Task<AttachmentDto> GetImageFileAsync(Guid id, string imageFileName);
    }
}
