using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Product
{
    public class UpdateProductPhotosDto : AddProductPhotosDto
    {
    }
    public class UpdateProductPhotoDto
    {
        public IFormFile? Photo { get; set; }
    }
}
