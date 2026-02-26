namespace RealTimeOrderEngine.Shared.DTOs.Orders;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
}