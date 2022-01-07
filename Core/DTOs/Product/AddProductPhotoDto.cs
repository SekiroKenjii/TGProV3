using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Product
{
    public class AddProductPhotoDto
    {
        public Guid ProductId { get; set; }
        public List<IFormFile>? Photos { get; set; }
    }
}
