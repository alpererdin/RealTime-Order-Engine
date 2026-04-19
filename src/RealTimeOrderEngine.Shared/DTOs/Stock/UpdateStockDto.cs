using System.ComponentModel.DataAnnotations;

namespace RealTimeOrderEngine.Shared.DTOs.Stock;

public class UpdateStockDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(0, 100000)]
    public int StockQuantity { get; set; }

    public bool IsStockTracked { get; set; }
}
