using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Brand
{
    public class AddBrandDto : BasePropertiesDto
    {
        public IFormFile? Photo { get; set; }
    }
}
