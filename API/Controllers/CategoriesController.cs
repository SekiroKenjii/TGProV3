using Core.Constants;
using Core.DTOs.Category;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return HandleResult(await _categoryService.GetCategory(id));
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return HandleResult(await _categoryService.GetCategories());
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto categoryDto)
        {
            CategoryValidator validator = new();

            var validationResult = validator.Validate(categoryDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _categoryService.AddCategory(categoryDto), Applications.Actions.Add);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto categoryDto)
        {
            CategoryValidator validator = new();

            var validationResult = validator.Validate(categoryDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _categoryService.UpdateCategory(id, categoryDto), Applications.Actions.Update);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            return HandleResult(await _categoryService.DeleteCategory(id), Applications.Actions.Delete);
        }
    }
}
