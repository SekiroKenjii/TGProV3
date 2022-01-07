using Core.DTOs.Product;
using FluentValidation;

namespace Core.Validators
{
    public class ProductPhotosValidator : AbstractValidator<AddProductPhotosDto>
    {
        public ProductPhotosValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty();
            RuleFor(p => p.Photos).NotNull();
        }
    }
}
