using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Product
{
    public class UpdateProductPhotoDto
    {
        public IFormFile? Photo { get; set; }
    }
}
