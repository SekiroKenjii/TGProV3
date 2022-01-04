using Core.DTOs.Condition;
using FluentValidation;

namespace Core.Validators
{
    public class ConditionValidator : AbstractValidator<AddConditionDto>
    {
        public ConditionValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1, 50);
            RuleFor(p => p.Description).Length(1, 500);
        }
    }
}
