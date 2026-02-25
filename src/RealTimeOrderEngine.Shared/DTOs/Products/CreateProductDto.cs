namespace RealTimeOrderEngine.Shared.DTOs.Products;

public class CreateProductDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}