using Core.DTOs.Category;
using FluentValidation;

namespace Core.Validators
{
    public class CategoryValidator : AbstractValidator<AddCategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1,50);
            RuleFor(p => p.Description).Length(1,500);
        }
    }
}
