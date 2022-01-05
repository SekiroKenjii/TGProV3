using Core.DTOs.Product;

namespace Core.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProduct(Guid productId);
        Task<List<ProductDto>> GetProducts();
        Task<List<CompactProductDto>> GetProductsPublic();
        Task<ProductDto> AddProduct(AddProductDto productDto);
        Task<bool> UpdateProduct(Guid productId, UpdateProductDto productDto);
        Task<bool> DeleteProduct(Guid productId);
    }
}
