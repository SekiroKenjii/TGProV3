using Core.DTOs.Product;
using FluentValidation;

namespace Core.Validators
{
    public class ProductValidator : AbstractValidator<AddProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.SerialNumber).NotEmpty().Length(1, 500);
            RuleFor(p => p.Name).NotEmpty().Length(1, 100);
            RuleFor(p => p.Specification).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Warranty).NotEmpty().Length(1, 100);
            RuleFor(p => p.Price).NotEmpty().GreaterThanOrEqualTo(0.00);
            RuleFor(p => p.UnitsInStock).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(p => p.UnitsOnOrder).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(p => p.Discontinued).NotEmpty();
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.SubBrandId).NotEmpty();
            RuleFor(p => p.ConditionId).NotEmpty();
            RuleFor(p => p.ProductTypeId).NotEmpty();
        }
    }
}
