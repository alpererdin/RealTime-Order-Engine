namespace RealTimeOrderEngine.Shared.DTOs.Stock;

public class UpdateStockDto
{
    public Guid ProductId { get; set; }
    public int StockQuantity { get; set; }
    public bool IsStockTracked { get; set; }
}