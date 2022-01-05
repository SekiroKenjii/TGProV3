using Core.DTOs.Brand;

namespace Core.Services
{
    public interface IBrandService
    {
        Task<BrandDto> GetBrand(Guid brandId);
        Task<List<BrandDto>> GetBrands();
        Task<List<CompactBrandDto>> GetBrandsPublic();
        Task<BrandDto> AddBrand(AddBrandDto brandDto);
        Task<bool> UpdateBrand(Guid brandId, UpdateBrandDto brandDto);
        Task<bool> DeleteBrand(Guid brandId);
    }
}
