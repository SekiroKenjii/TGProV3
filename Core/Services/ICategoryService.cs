using Core.DTOs.Category;

namespace Core.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategory(Guid categoryId);
        Task<List<CategoryDto>> GetCategories();
        Task<List<CompactCategoryDto>> GetCategoriesPublic();
        Task<CategoryDto> AddCategory(AddCategoryDto categoryDto);
        Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto categoryDto);
        Task<bool> DeleteCategory(Guid categoryId);
    }
}
