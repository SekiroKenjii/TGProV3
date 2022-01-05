using Core.DTOs.ProductType;
using FluentValidation;

namespace Core.Validators
{
    public class ProductTypeValidator : AbstractValidator<AddProductTypeDto>
    {
        public ProductTypeValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(1, 50);
            RuleFor(p => p.Description).Length(1, 500);
        }
    }
}
