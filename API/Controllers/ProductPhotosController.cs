using Core.DTOs.Product;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/internal/product-photos")]
    public class ProductPhotosController : BaseApiController
    {
        private readonly IProductPhotoService _productPhotoService;
        public ProductPhotosController(IProductPhotoService productPhotoService)
        {
            _productPhotoService = productPhotoService;
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductPhotos(Guid id)
        {
            return HandleResult(await _productPhotoService.GetProductPhotos(id));
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddProductPhotos([FromForm] AddProductPhotosDto productPhotosDto)
        {
            AddProductPhotosValidator validator = new();

            var validationResult = validator.Validate(productPhotosDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _productPhotoService.AddProductPhotos(productPhotosDto));
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProductPhotos(Guid id, [FromForm] UpdateProductPhotoDto productPhotoDto)
        {
            UpdateProductPhotoValidator validator = new();

            var validationResult = validator.Validate(productPhotoDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _productPhotoService.UpdateProductPhoto(id, productPhotoDto));
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductPhotos(Guid id)
        {
            return HandleResult(await _productPhotoService.DeleteProductPhoto(id));
        }
    }
}
