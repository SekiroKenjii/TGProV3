using Core.DTOs.SubBrand;

namespace Core.Services
{
    public interface ISubBrandService
    {
        Task<SubBrandDto> GetSubBrand(Guid subBrandId);
        Task<List<SubBrandDto>> GetSubBrands();
        Task<SubBrandDto> AddSubBrand(AddSubBrandDto subBrandDto);
        Task<bool> UpdateSubBrand(Guid subBrandId, UpdateSubBrandDto subBrandDto);
        Task<bool> DeleteSubBrand(Guid subBrandId);
    }
}