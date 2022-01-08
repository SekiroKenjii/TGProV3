using Core.DTOs.Product;

namespace Core.Services
{
    public interface IProductPhotoService
    {
        Task<List<ProductPhotoDto>> GetProductPhotos(Guid productId);
        Task<bool> AddProductPhotos(AddProductPhotosDto producPhotoDto);
        Task<bool> UpdateProductPhoto(Guid productPhotoId, UpdateProductPhotoDto producPhotoDto);
        Task<bool> DeleteProductPhoto(Guid productPhotoId);
    }
}
