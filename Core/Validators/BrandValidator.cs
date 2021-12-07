using Core.DTOs.Brand;
using FluentValidation;

namespace Core.Validators
{
    public class BrandValidator : AbstractValidator<AddBrandDto>
    {
        public BrandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1,50);
            RuleFor(p => p.Description).Length(0,500);
        }
    }
}
