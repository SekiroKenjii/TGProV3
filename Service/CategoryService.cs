using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Category;
using Core.Exceptions;
using Core.Services;
using Domain.Entities;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<CategoryDto> AddCategory(AddCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            category.CreatedBy = _userAccessor.GetUserId();

            await _unitOfWork.Categories.AddAsync(category);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSaved) throw new BadRequestException(Messages.ADD_FAILURE);

            var result = _mapper.Map<CategoryDto>(category);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(category.CreatedAt, category.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(category.LastModifiedAt, category.LastModifiedBy);

            return result;
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (category == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("Category"));

            _unitOfWork.Categories.Delete(category);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new BadRequestException(Messages.DELETE_FAILURE) : true;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            var result = _mapper.Map<List<CategoryDto>>(categories);

            for (int i = 0; i < categories.Count; i++)
            {
                result[i].CreatedInfo = await _userAccessor.GetCreatedInfo(categories[i].CreatedAt, categories[i].CreatedBy);
                result[i].LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(categories[i].LastModifiedAt, categories[i].LastModifiedBy);
            }

            return result;
        }

        public async Task<CategoryDto> GetCategory(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (category == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("Category"));

            var result = _mapper.Map<CategoryDto>(category);

            result.CreatedInfo = await _userAccessor.GetCreatedInfo(category.CreatedAt, category.CreatedBy);

            result.LastModifiedInfo = await _userAccessor.GetLastModifiedInfo(category.LastModifiedAt, category.LastModifiedBy);

            return result;
        }

        public async Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto categoryDto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (category == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("Category"));

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.LastModifiedAt = DateTime.UtcNow;
            category.LastModifiedBy = _userAccessor.GetUserId();

            _unitOfWork.Categories.Update(category);

            bool isSaved = await _unitOfWork.SaveChangeAsync() > 0;

            return !isSaved ? throw new BadRequestException(Messages.UPDATE_FAILURE) : true;
        }
    }
}
