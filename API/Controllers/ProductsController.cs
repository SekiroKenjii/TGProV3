using Core.Constants;
using Core.DTOs.Product;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("/api/[controller]")]
        public async Task<IActionResult> GetProductsPublic()
        {
            return HandleResult(await _productService.GetProductsPublic());
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            return HandleResult(await _productService.GetProduct(id));
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return HandleResult(await _productService.GetProducts());
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto productDto)
        {
            ProductValidator validator = new();

            var validationResult = validator.Validate(productDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _productService.AddProduct(productDto), Applications.Actions.Add);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto productDto)
        {
            ProductValidator validator = new();

            var validationResult = validator.Validate(productDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _productService.UpdateProduct(id, productDto), Applications.Actions.Update);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return HandleResult(await _productService.DeleteProduct(id), Applications.Actions.Delete);
        }
    }
}
