using Core.Constants;
using Core.DTOs.SubBrand;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SubBrandsController : BaseApiController
    {
        private readonly ISubBrandService _subBrandService;
        public SubBrandsController(ISubBrandService subBrandService)
        {
            _subBrandService = subBrandService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubBrand(Guid id)
        {
            return HandleResult(await _subBrandService.GetSubBrand(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetSubBrands()
        {
            return HandleResult(await _subBrandService.GetSubBrands());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddSubBrand([FromBody] AddSubBrandDto subBrandDto)
        {
            SubBrandValidator validator = new();

            var validationResult = validator.Validate(subBrandDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _subBrandService.AddSubBrand(subBrandDto), Applications.Actions.Add);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSubBrand(Guid id, [FromBody] UpdateSubBrandDto subBrandDto)
        {
            SubBrandValidator validator = new();

            var validationResult = validator.Validate(subBrandDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _subBrandService.UpdateSubBrand(id, subBrandDto), Applications.Actions.Update);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubBrand(Guid id)
        {
            return HandleResult(await _subBrandService.DeleteSubBrand(id), Applications.Actions.Delete);
        }
    }
}