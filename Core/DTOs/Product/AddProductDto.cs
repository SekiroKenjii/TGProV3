namespace Core.DTOs.Product
{
    public class AddProductDto : BasePropertiesDto
    {
        public string? SerialNumber { get; set; }
        public string? Specification { get; set; }
        public string? Warranty { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ConditionId { get; set; }
        public Guid? SubBrandId { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}
