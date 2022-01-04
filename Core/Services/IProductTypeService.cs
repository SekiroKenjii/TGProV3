using Core.DTOs.ProductType;

namespace Core.Services
{
    public interface IProductTypeService
    {
        Task<ProductTypeDto> GetProductType(Guid productTypeId);
        Task<List<ProductTypeDto>> GetProductTypes();
        Task<ProductTypeDto> AddProductType(AddProductTypeDto productTypeDto);
        Task<bool> UpdateProductType(Guid productTypeId, UpdateProductTypeDto productTypeDto);
        Task<bool> DeleteProductType(Guid productTypeId);
    }
}
