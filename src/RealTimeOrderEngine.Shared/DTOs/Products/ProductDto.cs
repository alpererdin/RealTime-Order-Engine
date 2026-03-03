namespace RealTimeOrderEngine.Shared.DTOs.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsAvailable { get; set; }

    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}