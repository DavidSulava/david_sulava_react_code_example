using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IProductVersionService
    {
        Task<Guid> CreateProductVersionAsync(ProductVersionCreateDto productVersion);

        Task UpdateProductVersionAsync(ProductVersionUpdateDto productVersion);

        Task RemoveProductVersionAsync(Guid id);

        Task<ICollection<ProductVersionDto>> GetProductVersionsByProductAsync(Guid productId);

        Task<ProductVersionDto> GetProductVersionAsync(Guid id);

        Task<AttachmentDto> GetModelFileAsync(Guid id);

        Task<AttachmentDto> GetImageFileAsync(Guid id, string imageFileName);
    }
}
