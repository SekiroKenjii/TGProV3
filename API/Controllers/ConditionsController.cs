using Core.Constants;
using Core.DTOs.Condition;
using Core.Exceptions;
using Core.Services;
using Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ConditionsController : BaseApiController
    {
        private readonly IConditionService _conditionService;
        public ConditionsController(IConditionService conditionService)
        {
            _conditionService = conditionService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCondition(Guid id)
        {
            return HandleResult(await _conditionService.GetCondition(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetConditions()
        {
            return HandleResult(await _conditionService.GetConditions());
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPost]
        public async Task<IActionResult> AddCondition([FromBody] AddConditionDto conditionDto)
        {
            ConditionValidator validator = new();

            var validationResult = validator.Validate(conditionDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _conditionService.AddCondition(conditionDto), Applications.Actions.Add);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCondition(Guid id, [FromBody] UpdateConditionDto conditionDto)
        {
            ConditionValidator validator = new();

            var validationResult = validator.Validate(conditionDto);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            return HandleResult(await _conditionService.UpdateCondition(id, conditionDto), Applications.Actions.Update);
        }

        [Authorize(Policy = "IsModerator")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCondition(Guid id)
        {
            return HandleResult(await _conditionService.DeleteCondition(id), Applications.Actions.Delete);
        }
    }
}
