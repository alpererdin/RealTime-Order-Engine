using System.ComponentModel.DataAnnotations;

namespace RealTimeOrderEngine.Shared.DTOs.Products;

public class UpdateProductDto
{
    public Guid Id { get; set; }

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

    public bool IsAvailable { get; set; }
}
