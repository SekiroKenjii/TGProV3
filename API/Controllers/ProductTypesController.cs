using Core.Constants;
using Core.DTOs.ProductType;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/internal/product-types")]
    public class ProductTypesController : BaseApiController
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypesController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductType(Guid id)
        {
            return HandleResult(await _productTypeService.GetProductType(id));
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        public async Task<IActionResult> GetProductTypes()
        {
            return HandleResult(await _productTypeService.GetProductTypes());
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddProductType([FromBody] AddProductTypeDto productTypeDto)
        {
            ProductTypeValidator validator = new();

            var validationResult = validator.Validate(productTypeDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _productTypeService.AddProductType(productTypeDto), Applications.Actions.Add);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProductType(Guid id, [FromBody] UpdateProductTypeDto productTypeDto)
        {
            ProductTypeValidator validator = new();

            var validationResult = validator.Validate(productTypeDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _productTypeService.UpdateProductType(id, productTypeDto), Applications.Actions.Update);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductType(Guid id)
        {
            return HandleResult(await _productTypeService.DeleteProductType(id), Applications.Actions.Delete);
        }
    }
}
