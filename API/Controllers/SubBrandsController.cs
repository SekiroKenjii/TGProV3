using Core.Constants;
using Core.DTOs.SubBrand;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/internal/sub-brands")]
    public class SubBrandsController : BaseApiController
    {
        private readonly ISubBrandService _subBrandService;
        public SubBrandsController(ISubBrandService subBrandService)
        {
            _subBrandService = subBrandService;
        }

        [HttpGet]
        [Route("/api/sub-brands")]
        public async Task<IActionResult> GetSubBrandsPublic()
        {
            return HandleResult(await _subBrandService.GetSubBrandsPublic());
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubBrand(Guid id)
        {
            return HandleResult(await _subBrandService.GetSubBrand(id));
        }

        [Authorize(Policy = "IsStaff")]
        [HttpGet]
        public async Task<IActionResult> GetSubBrands()
        {
            return HandleResult(await _subBrandService.GetSubBrands());
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddSubBrand([FromBody] AddSubBrandDto subBrandDto)
        {
            SubBrandValidator validator = new();

            var validationResult = validator.Validate(subBrandDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _subBrandService.AddSubBrand(subBrandDto), Applications.Actions.Add);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSubBrand(Guid id, [FromBody] UpdateSubBrandDto subBrandDto)
        {
            SubBrandValidator validator = new();

            var validationResult = validator.Validate(subBrandDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _subBrandService.UpdateSubBrand(id, subBrandDto), Applications.Actions.Update);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubBrand(Guid id)
        {
            return HandleResult(await _subBrandService.DeleteSubBrand(id), Applications.Actions.Delete);
        }
    }
}