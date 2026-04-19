using System.ComponentModel.DataAnnotations;

namespace RealTimeOrderEngine.Shared.DTOs.Products;

public class CreateProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Url]
    [StringLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999")]
    public decimal Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Range(0, 100000)]
    public int StockQuantity { get; set; }

    public bool IsStockTracked { get; set; }
}
