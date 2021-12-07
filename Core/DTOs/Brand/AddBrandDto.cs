using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Brand
{
    public class AddBrandDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
