using Core.DTOs.SubBrand;
using FluentValidation;

namespace Core.Validators
{
    public class SubBrandValidator : AbstractValidator<AddSubBrandDto>
    {
        public SubBrandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1, 50);
            RuleFor(p => p.Description).Length(0, 500);
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.BrandId).NotEmpty();
        }
    }
}
