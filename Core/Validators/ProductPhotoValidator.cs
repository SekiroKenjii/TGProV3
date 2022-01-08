using Core.DTOs.Product;
using FluentValidation;

namespace Core.Validators
{
    public class AddProductPhotosValidator : AbstractValidator<AddProductPhotosDto>
    {
        public AddProductPhotosValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty();
            RuleFor(p => p.Photos).NotEmpty();
        }
    }

    public class UpdateProductPhotoValidator : AbstractValidator<UpdateProductPhotoDto>
    {
        public UpdateProductPhotoValidator()
        {
            RuleFor(p => p.Photo).NotEmpty();
        }
    }
}
