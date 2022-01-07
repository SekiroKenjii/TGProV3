using Core.DTOs.Product;

namespace Core.Services
{
    public interface IProductPhotoService
    {
        Task<List<ProductPhotoDto>> GetProductPhotos(Guid productId);
        Task<List<ProductPhotoDto>> AddProductPhotos(AddProductPhotosDto producPhotoDto);
        Task<List<ProductPhotoDto>> UpdateProductPhotos(UpdateProductPhotosDto producPhotoDto);
        Task<bool> UpdateProductPhoto(Guid productPhotoId, UpdateProductPhotoDto producPhotoDto);
        Task<bool> DeleteProductPhoto(Guid productPhotoId);
    }
}
