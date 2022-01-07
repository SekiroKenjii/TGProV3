using Core.DTOs.Product;
using FluentValidation;

namespace Core.Validators
{
    public class ProductPhotoValidator : AbstractValidator<UpdateProductPhotoDto>
    {
        public ProductPhotoValidator()
        {
            RuleFor(p => p.Photo).NotNull();
        }
    }
}
