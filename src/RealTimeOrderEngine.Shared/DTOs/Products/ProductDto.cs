namespace RealTimeOrderEngine.Shared.DTOs.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsAvailable { get; set; }

    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }

    public int StockQuantity { get; set; }
    public bool IsStockTracked { get; set; }
}