using Core.Constants;
using Core.DTOs.Brand;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandsController : BaseApiController
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBrand(Guid id)
        {
            return HandleResult(await _brandService.GetBrand(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            return HandleResult(await _brandService.GetBrands());
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddBrand([FromForm] AddBrandDto brandDto)
        {
            BrandValidator validator = new();

            var validationResult = validator.Validate(brandDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _brandService.AddBrand(brandDto), Applications.Actions.Add);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromForm] UpdateBrandDto brandDto)
        {
            BrandValidator validator = new();

            var validationResult = validator.Validate(brandDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _brandService.UpdateBrand(id, brandDto), Applications.Actions.Update);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            return HandleResult(await _brandService.DeleteBrand(id), Applications.Actions.Delete);
        }
    }
}
